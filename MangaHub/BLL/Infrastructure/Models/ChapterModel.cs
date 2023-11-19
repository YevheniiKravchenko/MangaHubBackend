using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BLL.Infrastructure.Models
{
    public class ChapterModel
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
    }
}
