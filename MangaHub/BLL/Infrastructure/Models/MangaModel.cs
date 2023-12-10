using Common.Constants;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BLL.Infrastructure.Models
{
    public class MangaModel
    {
        public Guid MangaId { get; set; } = Guid.NewGuid();

        [Required]
        [MinLength(ValidationConstant.TitleMinLength)]
        [MaxLength(ValidationConstant.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public Dictionary<Genre, string> Genres { get; set; }

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

        public double Rating { get; set; }

        public double? CurrentUserMark { get; set; }
    }
}
