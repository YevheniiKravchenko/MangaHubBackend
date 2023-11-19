using BLL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Contracts
{
    public interface IChapterService
    {
        IEnumerable<ChapterListItem> GetAllMangaChapters(Guid mangaId);

        ChapterModel GetById(Guid chapterId);

        void Delete(Guid chapterId);

        void Apply(ChapterModel chapterModel);

        void UploadChapter(byte[] chapterData, Guid chapterId);
    }
}