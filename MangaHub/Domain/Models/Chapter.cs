using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Chapter
    {
        public Guid ChapterId { get; set; }

        [Required]
        [MinLength(ValidationConstant.TitleMinLength)]
        [MaxLength(ValidationConstant.TitleMaxLength)]
        public string Title { get; set; }

        public byte[] Scans { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        [Required]
        public Guid MangaId { get; set; }

        #region Relations

        [JsonIgnore]
        public Manga Manga { get; set; }

        #endregion
    }
}