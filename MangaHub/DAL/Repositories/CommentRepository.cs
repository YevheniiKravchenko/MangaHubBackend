using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContextBase _context;
        private readonly DbSet<Comment> _comments;

        public CommentRepository(DbContextBase context)
        {
            _context = context;

            _comments = _context.Comments;
        }

        public Comment Create(Comment newComment)
        {
            newComment.UpdatedDate = DateTime.Now;

            var comment = _comments.FirstOrDefault(c => c.Id == newComment.Id);

            if (comment is null)
            {
                newComment.CreatedDate = DateTime.Now;

                _comments.Add(newComment);
                _context.Commit();
                return newComment;
            }

            newComment.MangaId = comment.MangaId;
            newComment.UserId = comment.UserId;
            newComment.CreatedDate = comment.CreatedDate;

            var commentEntry = _context.Entry(comment);
            commentEntry.CurrentValues.SetValues(newComment);
            _context.Commit();
            return comment;
        }

        public int Delete(int commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);

            if (comment is null)
            {
                return commentId;
            }

            _comments.RemoveRange(
                _comments.Where(c => c.ParentCommentId == commentId)
                );

            _comments.Remove(comment);
            _context.Commit();
            return commentId;
        }

        public Comment? GetById(int commentId)
        {
            return _comments
                .Include(c => c.ChildComments)
                .FirstOrDefault(c => c.Id == commentId);
        }

        public IEnumerable<Comment> GetMangaComments(Guid mangaId)
        {
            return _comments
                .Include(c => c.ChildComments)
                .Where(c => c.MangaId == mangaId)
                .ToList();
        }

        public Comment Update(Comment newComment)
        {
            return Create(newComment);
        }
    }
}
