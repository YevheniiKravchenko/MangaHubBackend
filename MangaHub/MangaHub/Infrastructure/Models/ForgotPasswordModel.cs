using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [MinLength(ValidationConstant.EmailMinLength)]
        public string Email { get; set; }
    }
}
