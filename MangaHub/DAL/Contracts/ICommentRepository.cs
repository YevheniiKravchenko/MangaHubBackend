using Domain.Models;

namespace DAL.Contracts
{
    public interface ICommentRepository
    {
        Comment Create(Comment newComment);
        int Delete(int commentId);
        Comment? GetById(int commentId);
        IEnumerable<Comment> GetMangaComments(Guid mangaId);
        Comment Update(Comment newComment);
    }
}
