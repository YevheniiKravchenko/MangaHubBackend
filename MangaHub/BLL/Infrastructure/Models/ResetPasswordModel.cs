using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [MinLength(ValidationConstant.PasswordMinLength)]
        [MaxLength(ValidationConstant.PasswordMaxLength)]
        [RegularExpression(RegularExpressions.Password,
            ErrorMessage = "Password may contain only latin characters, numbers and special characters")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}