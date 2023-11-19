using Domain.Models;

namespace DAL.Contracts
{
    public interface IChapterRepository
    {
        IQueryable<Chapter> GetAll();

        Chapter GetById(Guid chapterId);

        void Delete(Guid chapterId);

        void Apply(Chapter chapter);

        void UploadChapter(byte[] chapterData, Guid chapterId);
    }
}