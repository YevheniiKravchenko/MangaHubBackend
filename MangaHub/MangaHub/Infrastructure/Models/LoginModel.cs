using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)]
        public string Login { get; set; }

        [Required]
        [MinLength(ValidationConstant.PasswordMinLength)]
        public string Password { get; set; }
    }
}