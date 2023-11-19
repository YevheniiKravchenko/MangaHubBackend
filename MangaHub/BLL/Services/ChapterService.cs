using AutoMapper;
using BLL.Contracts;
using BLL.Infrastructure.Models;
using DAL.Contracts;
using Domain.Models;

namespace BLL.Services
{
    public class ChapterService : IChapterService
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IMapper> _mapper;

        public ChapterService(
            Lazy<IUnitOfWork> unitOfWork,
            Lazy<IMapper> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Apply(ChapterModel chapterModel)
        {
            var chapter = _mapper.Value.Map<Chapter>(chapterModel);

            _unitOfWork.Value.Chapters.Value.Apply(chapter);
        }

        public void Delete(Guid chapterId)
        {
            _unitOfWork.Value.Chapters.Value.Delete(chapterId);
        }

        public IEnumerable<ChapterListItem> GetAllMangaChapters(Guid mangaId)
        {
            var chapters = _unitOfWork.Value.Chapters.Value.GetAll()
                .Where(ch => ch.MangaId == mangaId)
                .ToList();

            var chapterList = _mapper.Value.Map<List<ChapterListItem>>(chapters);

            return chapterList;
        }

        public ChapterModel GetById(Guid chapterId)
        {
            var chapter = _unitOfWork.Value.Chapters.Value.GetById(chapterId);
            var chapterModel = _mapper.Value.Map<ChapterModel>(chapter);

            return chapterModel;
        }

        public void UploadChapter(byte[] chapterData, Guid chapterId)
        {
            _unitOfWork.Value.Chapters.Value.UploadChapter(chapterData, chapterId);
        }
    }
}