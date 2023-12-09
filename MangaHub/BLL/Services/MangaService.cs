using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Contracts;
using DAL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Services
{
    public class MangaService : IMangaService
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IMapper> _mapper;

        public MangaService(Lazy<IUnitOfWork> unitOfWork, Lazy<IMapper> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Apply(MangaModel mangaModel)
        {
            var manga = _mapper.Value.Map<Manga>(mangaModel);
            _unitOfWork.Value.Mangas.Value.Apply(manga);
        }

        public void Delete(Guid mangaId)
        {
            _unitOfWork.Value.Mangas.Value.Delete(mangaId);
        }

        public IEnumerable<MangaListItem> GetAll()
        {
            var mangas = _unitOfWork.Value.Mangas.Value.GetAll()
                .ToList();

            var mangaList = _mapper.Value.Map<List<MangaListItem>>(mangas);

            return mangaList;
        }

        public IEnumerable<MangaListItem> GetAll(MangaFilter filter)
        {
            var mangas = _unitOfWork.Value.Mangas.Value.GetAll(filter)
                .ToList();

            var mangaList = _mapper.Value.Map<List<MangaListItem>>(mangas);

            return mangaList;
        }

        public MangaModel GetById(Guid mangaId, int currentUserId)
        {
            var manga = _unitOfWork.Value.Mangas.Value.GetById(mangaId);
            var mangaModel = _mapper.Value.Map<MangaModel>(manga);

            var currentUserMark = manga.Ratings
                    .FirstOrDefault(x => x.UserId == currentUserId)
                    ?.Mark;

            mangaModel.CurrentUserMark = currentUserMark;

            return mangaModel;
        }

        public void SetRating(Guid mangaId, int userId, int mark)
        {
            _unitOfWork.Value.Mangas.Value.SetRating(mangaId, userId, mark);
        }

        public void UploadCoverImage(byte[] imageBytes, Guid mangaId)
        {
            _unitOfWork.Value.Mangas.Value.UploadCoverImage(imageBytes, mangaId);
        }
    }
}