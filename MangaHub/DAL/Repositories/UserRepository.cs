using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Helpers;
using DAL.Infrastructure.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextBase _dbContext;
        private readonly Lazy<IMapper> _mapper;

        private readonly DbSet<User> _users;
        private readonly DbSet<UserProfile> _userProfiles;
        private readonly DbSet<RefreshToken> _refreshTokens;

        public UserRepository(
            DbContextBase dbContext,
            Lazy<IMapper> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _users = dbContext.Users;
            _userProfiles = dbContext.Profiles;
            _refreshTokens = dbContext.RefreshTokens;
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
            return _users.AsQueryable()
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
            var user = _users.FirstOrDefault(x => x.Login == login);

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

        public void ResetPassword(int userId, Guid guid, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ResetPasswordRequest(int userId)
        {
            throw new NotImplementedException();
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
    }
}