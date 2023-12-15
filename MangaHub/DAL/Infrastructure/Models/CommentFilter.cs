using Domain.Models;

namespace DAL.Infrastructure.Models
{
    public class CommentFilter : FilterBase<Comment>
    {
        public Guid MangaId { get; set; }
    }
}
