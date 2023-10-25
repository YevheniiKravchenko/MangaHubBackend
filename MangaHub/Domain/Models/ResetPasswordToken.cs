using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class ResetPasswordToken
    {
        public Guid ResetPasswordTokenId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpiresOnUtc { get; set; }

        [Required]
        public int UserId { get; set; }

        #region Relations

        [JsonIgnore]
        public User User { get; set; }

        #endregion
    }
}