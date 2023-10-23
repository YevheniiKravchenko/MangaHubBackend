using DAL.Infrastructure.Models;
using Domain.Models;

namespace DAL.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll(PagingModel pagingModel);

        User GetUserById(int userId);

        void UpdateUserProfileInfo(UserProfileInfo model);

        User LoginUser(string login, string password);

        void RegisterUser(RegisterUserModel model);

        void ResetPasswordRequest(int userId);

        void ResetPassword(int userId, Guid guid, string newPassword);

        Guid CreateRefreshToken(int userId, int shiftInSeconds);

        User GetUserByRefreshToken(Guid refreshToken);
    }
}