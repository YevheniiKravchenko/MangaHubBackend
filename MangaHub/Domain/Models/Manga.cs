using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Manga
    {
        public Guid MangaId { get; set; }

        [Required]
        [MinLength(ValidationConstant.TitleMinLength)]
        [MaxLength(ValidationConstant.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public int Genre { get; set; }

        [Required]
        [MinLength(ValidationConstant.DescriptionMinLength)]
        [MaxLength(ValidationConstant.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleasedOn { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public byte[] CoverImage { get; set; }

        public int? UserId { get; set; }

        #region Relations

        [JsonIgnore]
        public ICollection<Chapter> Chapters { get; set; }

        [JsonIgnore]
        public ICollection<Rating> Ratings { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        #endregion
    }
}