using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(ValidationConstant.LoginMinLength)]
        [MaxLength(ValidationConstant.LoginMaxLength)]
        [RegularExpression(RegularExpressions.Login,
            ErrorMessage = "Login may contain only latin characters, numbers, hyphens and underscores")]
        public string Login { get; set; }

        [Required]
        [MinLength(ValidationConstant.PasswordMinLength)]
        [MaxLength(ValidationConstant.PasswordMaxLength)]
        [RegularExpression(RegularExpressions.Password,
            ErrorMessage = "Password may contain only latin characters, numbers and special characters")]
        public string Password { get; set; }
    }
}