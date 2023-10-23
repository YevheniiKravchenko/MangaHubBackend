using BLL.Contracts;
using DAL.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<IUserService> _userService;

        public UserController(Lazy<IUserService> userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegisterUserModel registerModel)
        {
            _userService.Value.RegisterUser(registerModel);

            return Ok();
        }
    }
}
