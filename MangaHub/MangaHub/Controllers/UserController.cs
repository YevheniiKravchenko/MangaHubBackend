using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult RequestResetPassword(string token)
        {
            var isTokenValid = _userService.Value.IsResetPasswordTokenValid(token);

            if (isTokenValid)
                return Ok();

            return BadRequest("Invalid reset password token");
        }

        [HttpPost("reset-password")]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.ResetPassword(resetPasswordModel);

            return Ok();
        }

        [HttpGet("get-all")]
        [Authorize]
        public ActionResult GetAll(PagingModel paging)
        {
            var users = _userService.Value.GetAllUsers(paging);

            return Ok(users);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetUserProfileById(int userId)
        {
            var user = _userService.Value.GetUserProfileById(userId);

            return Ok(user);
        }

        [HttpPost("set-isadmin-value")]
        [Authorize(Roles = "Admin")]
        public ActionResult SetIsAdminValueForUser(int userId, bool isAdmin)
        {
            _userService.Value.SetIsAdminValueForUser(userId, isAdmin);

            return Ok();
        }
    }
}
