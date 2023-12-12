namespace BLL.Infrastructure.Models
{
    public class NewCommentModel
    {
        public string Body { get; set; } = string.Empty;
        public Guid? MangaId { get; set; }
        public int? ParentCommentId { get; set; }
        public int UserId { get; set; }
    }
}
