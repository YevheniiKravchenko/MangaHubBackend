using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Models;

public class UploadChapterModel
{
    [Required]
    public Guid ChapterId { get; set; }

    [Required]
    public IFormFile ChapterFile { get; set; }
}
