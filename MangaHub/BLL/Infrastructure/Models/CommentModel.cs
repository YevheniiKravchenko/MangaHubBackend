using Domain.Models;

namespace BLL.Infrastructure.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CommentModel> ChildComments { get; set; } = new List<CommentModel>();
    }
}
