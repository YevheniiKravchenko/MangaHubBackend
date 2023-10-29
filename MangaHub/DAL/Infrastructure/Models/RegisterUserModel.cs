using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace DAL.Infrastructure.Models
{
    public class RegisterUserModel
    {
        public int UserId { get; set; }

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

        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)]
        public string LastName { get; set; }

        public byte[] Avatar { get; set; }

        [Required]
        [MinLength(ValidationConstant.DescriptionMinLength)]
        [MaxLength(ValidationConstant.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(RegularExpressions.PhoneNumber,
            ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public bool ShowConfidentialInformation { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [MinLength(ValidationConstant.EmailMinLength)]
        public string Email { get; set; }
    }
}