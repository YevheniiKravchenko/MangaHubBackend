using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using Common.Configs;
using DAL.Contracts;
using DAL.Infrastructure.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AuthOptions _authOptions;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IMapper> _mapper;

        public UserService(
            AuthOptions authOptions,
            Lazy<IUnitOfWork> unitOfWork,
            Lazy<IMapper> mapper)
        {
            _authOptions = authOptions;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Guid CreateRefreshToken(int userId)
        {
            return _unitOfWork.Value.Users.Value.CreateRefreshToken(
                userId,
                _authOptions.RefreshTokenLifetime
            );
        }

        public IEnumerable<UserProfileModel> GetAllUsers(PagingModel pagingModel)
        {
            var users = _unitOfWork.Value.Users.Value
                .GetAll(pagingModel)
                .ToList();

            var userProfiles = _mapper.Value.Map<List<UserProfileModel>>(users);

            return userProfiles;
        }

        public UserModel GetUserByRefreshToken(Guid refreshToken)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserByRefreshToken(refreshToken);
            var userModel = _mapper.Value.Map<UserModel>(user);

            return userModel;
        }

        public UserProfileModel GetUserProfileById(int userId)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserById(userId);
            var userProfileModel = _mapper.Value.Map<UserProfileModel>(user);

            return userProfileModel;
        }

        public UserModel LoginUser(string login, string password)
        {
            var user = _unitOfWork.Value.Users.Value.LoginUser(login, password);
            var userModel = _mapper.Value.Map<UserModel>(user);

            return userModel;
        }

        public void RegisterUser(RegisterUserModel model)
        {
            _unitOfWork.Value.Users.Value.RegisterUser(model);
        }

        public void ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            _unitOfWork.Value.Users.Value.ResetPassword(resetPasswordModel.Token, resetPasswordModel.Password);
        }

        public void UpdateUserInfo(UserProfileInfo model)
        {
            _unitOfWork.Value.Users.Value.UpdateUserProfileInfo(model);
        }
        
        public bool IsResetPasswordTokenValid(string token)
        {
            return _unitOfWork.Value.Users.Value.IsResetPasswordTokenValid(token);
        }

        public void SetIsAdminValueForUser(int userId, bool isAdmin)
        {
            _unitOfWork.Value.Users.Value.SetIsAdminValueForUser(userId, isAdmin);
        }
    }
}
