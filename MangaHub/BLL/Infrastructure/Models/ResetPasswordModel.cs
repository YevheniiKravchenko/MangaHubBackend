using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
