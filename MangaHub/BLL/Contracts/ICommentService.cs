using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;

namespace BLL.Contracts
{
    public interface ICommentService
    {
        CommentModel Create(NewCommentModel newComment);
        CommentModel Update(UpdateCommentModel newComment);
        int Delete(int commentId);
        IEnumerable<CommentModel> GetMangaComments(CommentFilter commentFilter);
    }
}
