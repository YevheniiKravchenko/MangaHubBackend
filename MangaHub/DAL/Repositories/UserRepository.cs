using AutoMapper;
using DAL.Contracts;
using DAL.DbContexts;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextBase _dbContext;
        private readonly Lazy<IMapper> _mapper;

        private readonly DbSet<User> _users;
        private readonly DbSet<Person> _persons;
        private readonly DbSet<RefreshToken> _refreshTokens;

        public UserRepository(
            DbContextBase dbContext,
            Lazy<IMapper> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            _users = dbContext.Users;
            _persons = dbContext.Persons;
            _refreshTokens = dbContext.RefreshTokens;
        }

        public Guid CreateRefreshToken(int userId, int shiftInSeconds)
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

            return refreshToken.RefreshTokenId;
        }

        public IQueryable<User> GetAll()
        {
            return _users.AsQueryable();
        }

        public User GetUserById(int userId)
        {
            var user = _users
                .Include(u => u.Person)
                .FirstOrDefault(u => u.UserId == userId)
                    ?? throw new ArgumentException("INVALID_USERID");

            return user;
        }

        public int GetUserIdByRefreshToken(Guid refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(x =>
                x.RefreshTokenId == refreshToken
                && x.ExpiresOnUtc >= DateTime.UtcNow
            ) ?? throw new ArgumentException("INVALID_REFRESH_TOKEN");

            return token.UserId;
        }

        public int LoginUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(RegisterUserModel model)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(int userId, Guid guid, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void ResetPasswordRequest(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonInfo(PersonInfo model)
        {
            var person = _persons.FirstOrDefault(p => p.UserId == model.UserId)
                ?? throw new ArgumentException("INVALID_USERID");

            _mapper.Value.Map(model, person);
            _dbContext.Commit();
        }
    }
}