using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace DAL.Infrastructure.Models
{
    public class RegisterUserModel
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(ValidationConstant.NameMinLength)]
        [MaxLength(ValidationConstant.NameMaxLength)]
        public string Login { get; set; }

        [Required]
        [MinLength(ValidationConstant.PasswordMinLength)]
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
        public string PhoneNumber { get; set; }

        public bool ShowConfidentialInformation { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MinLength(ValidationConstant.EmailMinLength)]
        public string Email { get; set; }
    }
}