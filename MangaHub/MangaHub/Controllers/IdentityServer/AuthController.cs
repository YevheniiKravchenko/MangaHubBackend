using BLL.Contracts;
using Common.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Models;

namespace MangaHub.Controllers.IdentityServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthOptions _authOptions;

        private readonly IUserService _userService;

        public AuthController(
            AuthOptions authOptions,
            IUserService userService)
        {
            _authOptions = authOptions;
            _userService = userService;
        }

        [HttpGet("refresh")]
        public ActionResult Refresh([FromHeader] string refreshTokenString)
        {
            var refreshToken = refreshTokenString.DecodeToken();
            var userId = _userService.GetUserIdByRefreshToken(refreshToken);
            var token = GetToken(userId);

            return Ok(token);
        }

        #region Helpers

        private Token GetToken(int userId)
        {
            var authParams = _authOptions;
            var securityKey = authParams.SymmetricSecurityKey;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                new List<Claim>() {
                    new Claim("id", userId.ToString()),
                },
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var token = new Token()
            {
                AccessToken = tokenString,
                RefreshToken = _userService.CreateRefreshToken(userId).EncodeToken()
            };

            return token;
        }

        #endregion
    }
}
