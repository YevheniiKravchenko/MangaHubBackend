namespace WebAPI.Infrastructure.Models
{
    public class NewComment
    {
        public string Body { get; set; } = string.Empty;
        public int? ParentCommentId { get; set; }
        public Guid? MangaId { get; set; }
    }
}
