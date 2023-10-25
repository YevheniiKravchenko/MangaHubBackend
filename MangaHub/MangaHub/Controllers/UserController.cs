using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

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
            _userService.Value.RegisterUser(registerModel);

            return Ok();
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword([FromBody]string email)
        {
            _emailService.Value.SendResetPasswordEmail(email);

            return Ok();
        }

        [HttpGet("request-reset-password")]
        public ActionResult RequestResetPassword(string token)
        {
            var isTokenValid = _userService.Value.IsResetPasswordTokenValid(token);

            if (isTokenValid)
                return Ok();

            return BadRequest();
        }

        [HttpPost("reset-password")]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userService.Value.ResetPassword(resetPasswordModel);

            return Ok();
        }
    }
}
