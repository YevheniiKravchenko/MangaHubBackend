using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IEmailService> _emailService;

        public UserController(
            Lazy<IUserService> userService,
            Lazy<IEmailService> emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.RegisterUser(registerModel);

            return Ok();
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword([FromBody]ForgotPasswordModel forgotPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _emailService.Value.SendResetPasswordEmail(forgotPassword.Email);

            return Ok();
        }

        [HttpGet("request-reset-password")]
        public ActionResult RequestResetPassword([FromQuery]string token)
        {
            var isTokenValid = _userService.Value.IsResetPasswordTokenValid(token);

            if (isTokenValid)
                return Ok();

            return BadRequest("Invalid reset password token");
        }

        [HttpPost("reset-password")]
        public ActionResult ResetPassword([FromBody]ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.ResetPassword(resetPasswordModel);

            return Ok();
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAll([FromQuery]PagingModel paging)
        {
            var users = _userService.Value.GetAllUsers(paging);

            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUserProfileById([FromQuery]int userId)
        {
            var authUserId = this.GetCurrentUserId();
            if (!HttpContext.User.IsInRole("Admin") && authUserId != userId)
                return Forbid();

            var user = _userService.Value.GetUserProfileById(userId);

            return Ok(user);
        }

        [HttpPost("set-isadmin-value")]
        [Authorize(Roles = "Admin")]
        public ActionResult SetIsAdminValueForUser([FromBody]SetIsAdminValueForUserModel model)
        {
            _userService.Value.SetIsAdminValueForUser(model.UserId, model.IsAdmin);

            return Ok();
        }

        [HttpPost("update-user-profile")]
        [Authorize]
        public ActionResult UpdateUserProfile([FromBody] UserProfileInfo userProfileInfo)
        {
            var authUserId = this.GetCurrentUserId();
            if (!HttpContext.User.IsInRole("Admin") && authUserId != userProfileInfo.UserId)
                return Forbid();

            _userService.Value.UpdateUserInfo(userProfileInfo);

            return Ok();
        }
    }
}