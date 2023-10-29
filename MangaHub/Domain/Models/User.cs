using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(ValidationConstant.LoginMinLength)]
        [MaxLength(ValidationConstant.LoginMaxLength)]
        [RegularExpression(RegularExpressions.Login, 
            ErrorMessage = "Login may contain only latin characters, numbers, hyphens and underscores")]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public bool IsAdmin { get; set; }

        #region Relations

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }

        [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        [JsonIgnore]
        public ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }

        #endregion
    }
}