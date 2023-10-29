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

        void ResetPassword(ResetPasswordModel resetPasswordModel);

        Guid CreateRefreshToken(int userId);

        UserModel GetUserByRefreshToken(Guid refreshToken);

        bool IsResetPasswordTokenValid(string token);

        void SetIsAdminValueForUser(int userId, bool isAdmin);
    }
}