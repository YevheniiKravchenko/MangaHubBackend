using DAL.Infrastructure.Models;
using Domain.Models;

namespace DAL.Contracts
{
    public interface ICommentRepository
    {
        Comment Create(Comment newComment);
        int Delete(int commentId);
        Comment? GetById(int commentId);
        IEnumerable<Comment> GetMangaComments(CommentFilter commentFilter);
        Comment Update(Comment newComment);
    }
}
