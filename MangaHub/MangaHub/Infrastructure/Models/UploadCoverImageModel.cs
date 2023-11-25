using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Models;

public class UploadCoverImageModel
{
    [Required]
    public Guid MangaId { get; set; }

    [Required]
    public IFormFile CoverImage { get; set; }
}
