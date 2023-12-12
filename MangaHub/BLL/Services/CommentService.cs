using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Contracts;
using Domain.Models;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentService(
            IMapper mapper,
            ICommentRepository commentRepository
            )
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public CommentModel Create(NewCommentModel newComment)
        {
            if (newComment.ParentCommentId is not null)
            {
                newComment.MangaId = null;
            }
            var comment = _mapper.Map<Comment>(newComment);
            return _mapper.Map<CommentModel>(_commentRepository.Create(comment));
        }

        public int Delete(int commentId)
        {
            return _commentRepository.Delete(commentId);
        }

        public IEnumerable<CommentModel> GetMangaComments(Guid mangaId)
        {
            return _mapper.Map<IEnumerable<CommentModel>>(_commentRepository.GetMangaComments(mangaId));
        }

        public CommentModel Update(UpdateCommentModel newComment)
        {
            var comment = _mapper.Map<Comment>(newComment);
            return _mapper.Map<CommentModel>(_commentRepository.Update(comment));
        }
    }
}
