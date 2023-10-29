using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Helpers;
using DAL.Infrastructure.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextBase _dbContext;
        private readonly Lazy<IMapper> _mapper;

        private readonly DbSet<User> _users;
        private readonly DbSet<UserProfile> _userProfiles;
        private readonly DbSet<RefreshToken> _refreshTokens;
        private readonly DbSet<ResetPasswordToken> _resetPasswordTokens;

        public UserRepository(
            DbContextBase dbContext,
            Lazy<IMapper> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _users = dbContext.Users;
            _userProfiles = dbContext.Profiles;
            _refreshTokens = dbContext.RefreshTokens;
            _resetPasswordTokens = dbContext.ResetPasswordTokens;
        }

        public Guid CreateRefreshToken(int userId, int shiftInSeconds)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                _refreshTokens.RemoveRange(
                    _refreshTokens.Where(x => x.UserId == userId)
                );
                _dbContext.Commit();

                var refreshToken = new RefreshToken()
                {
                    UserId = userId,
                    ExpiresOnUtc = DateTime.UtcNow.AddSeconds(shiftInSeconds)
                };
                _refreshTokens.Add(refreshToken);
                _dbContext.Commit();

                scope.Commit();

                return refreshToken.RefreshTokenId;
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<User> GetAll(PagingModel pagingModel)
        {
            return _users.Include(u => u.UserProfile)
                .AsQueryable()
                .OrderBy(x => x.RegistrationDate)
                .GetPage(pagingModel);
        }

        public User GetUserById(int userId)
        {
            var user = _users
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.UserId == userId)
                    ?? throw new ArgumentException("INVALID_USERID");

            return user;
        }

        public User GetUserByRefreshToken(Guid refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(x =>
                x.RefreshTokenId == refreshToken
                && x.ExpiresOnUtc >= DateTime.UtcNow
            ) ?? throw new ArgumentException("INVALID_REFRESH_TOKEN");

            var user = _users.FirstOrDefault(x => x.UserId == token.UserId)
                ?? throw new ArgumentException("INVALID_USERID");

            return user;
        }

        public User LoginUser(string login, string password)
        {
            var user = _users.FirstOrDefault(x => x.Login == login
                || x.UserProfile.Email == login);

            if (user is null
                || !HashHelper.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            {
                throw new UnauthorizedAccessException();
            }

            return user;
        }

        public void RegisterUser(RegisterUserModel model)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                //TODO Think about unique email
                if (_users.Any(x => x.Login == model.Login))
                    throw new ArgumentException("LOGIN_EXISTS");

                if (_users.Any(x => x.UserProfile.Email == model.Email))
                    throw new ArgumentException("EMAIL_EXISTS");

                var (salt, passwordHash) = HashHelper.GenerateNewPasswordHash(model.Password);
                var user = _mapper.Value.Map<User>(model);
                var userProfile = _mapper.Value.Map<UserProfile>(model);
                if (userProfile.Avatar == null)
                    userProfile.Avatar = Array.Empty<byte>();

                user.PasswordSalt = salt;
                user.PasswordHash = passwordHash;
                user.RegistrationDate = DateTime.UtcNow;
                user.UserProfile = userProfile;

                _users.Add(user);
                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void ResetPassword(string token, string newPassword)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                var user = _users.Include(u => u.ResetPasswordTokens)
                .FirstOrDefault(u => u.ResetPasswordTokens
                    .Any(t => t.Token == token
                        && t.ExpiresOnUtc >= DateTime.UtcNow))
                ?? throw new ArgumentException("INVALID_RESET_PASSWORD_TOKEN");

                var (salt, passwordHash) = HashHelper.GenerateNewPasswordHash(newPassword);
                user.PasswordSalt = salt;
                user.PasswordHash = passwordHash;
                user.ResetPasswordTokens.Clear();

                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUserProfileInfo(UserProfileInfo model)
        {
            using var scope = _dbContext.Database.BeginTransaction();

            try
            {
                var profile = _userProfiles.FirstOrDefault(p => p.UserId == model.UserId)
                ?? throw new ArgumentException("INVALID_USERID");

                _mapper.Value.Map(model, profile);
                _dbContext.Commit();

                scope.Commit();
            }
            catch (Exception ex)
            {
                scope.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public User GetUserByEmail(string email)
        {
            var userProfile = _userProfiles
                .Include(p => p.User)
                .FirstOrDefault(p => p.Email == email);

            if (userProfile == null)
                throw new ArgumentException("USER_WITH_EMAIL_NOT_FOUND");

            return userProfile.User;
        }

        public bool IsResetPasswordTokenValid(string token)
        {
            var resetPasswordToken = _resetPasswordTokens.FirstOrDefault(x => x.Token == token
                && x.ExpiresOnUtc >= DateTime.UtcNow);

            return resetPasswordToken != null;
        }

        public string GenerateResetPasswordToken(int userId)
        {
            var scope = _dbContext.Database.BeginTransaction();

            try
            {
                _resetPasswordTokens.RemoveRange(
                    _resetPasswordTokens.Where(t => t.UserId == userId));
                _dbContext.Commit();

                var token = CreateRandomToken();
                var resetPasswordToken = new ResetPasswordToken()
                {
                    Token = token,
                    UserId = userId,
                    ExpiresOnUtc = DateTime.UtcNow.AddDays(1),
                };
                _resetPasswordTokens.Add(resetPasswordToken);

                _dbContext.Commit();
                
                scope.Commit();

                return token;
            }
            catch (Exception)
            {
                scope.Rollback();
                throw;
            }
        }

        public void SetIsAdminValueForUser(int userId, bool isAdmin)
        {
            var user = GetUserById(userId);

            user.IsAdmin = isAdmin;

            _dbContext.Commit();
        }

        private string CreateRandomToken()
        {
            var newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            while(_resetPasswordTokens.Any(t => t.Token == newToken))
                newToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            return newToken;
        }
    }
}