using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Rating
    {
        public Guid RatingId { get; set; }

        [Required]
        public byte Mark { get; set; }

        public int? UserId { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        #region Relations

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Manga Manga { get; set; }

        #endregion
    }
}