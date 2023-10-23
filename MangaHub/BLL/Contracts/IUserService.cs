using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;

namespace BLL.Contracts
{
    public interface IUserService
    {
        UserProfileModel GetUserProfileById(int userId);

        IEnumerable<UserProfileModel> GetAllUsers(PagingModel pagingModel);

        void UpdateUserInfo(UserProfileInfo model);

        UserModel LoginUser(string login, string password);

        void RegisterUser(RegisterUserModel model);

        void ResetPasswordRequest(int userId);

        void ResetPassword(int userId, Guid guid, string newPassword);

        Guid CreateRefreshToken(int userId);

        UserModel GetUserByRefreshToken(Guid refreshToken);
    }
}