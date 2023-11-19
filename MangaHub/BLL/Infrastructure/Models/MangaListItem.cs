namespace BLL.Infrastructure.Models
{
    public class MangaListItem
    {
        public Guid MangaId { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public byte[] CoverImage { get; set; }

        public double Rating { get; set; }
    }
}
