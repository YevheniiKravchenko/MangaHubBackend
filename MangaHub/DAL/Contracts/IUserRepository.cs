using DAL.Models;
using Domain.Models;

namespace DAL.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        User GetUserById(int userId);

        void UpdatePersonInfo(PersonInfo model);

        int LoginUser(string login, string password);

        void RegisterUser(RegisterUserModel model);

        void ResetPasswordRequest(int userId);

        void ResetPassword(int userId, Guid guid, string newPassword);

        Guid CreateRefreshToken(int userId, int shiftInSeconds);

        int GetUserIdByRefreshToken(Guid refreshToken);
    }
}