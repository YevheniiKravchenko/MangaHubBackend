using DAL.Contracts;
using DAL.DbContexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly DbContextBase _dbContext;

        private readonly DbSet<Chapter> _chapters;
        private readonly DbSet<Manga> _mangas;

        public ChapterRepository(DbContextBase dbContext)
        {
            _dbContext = dbContext;

            _chapters = dbContext.Chapters;
            _mangas = dbContext.Mangas;
        }

        public void Apply(Chapter chapter)
        {
            var manga = _mangas.FirstOrDefault(m => m.MangaId == chapter.MangaId)
                ?? throw new ArgumentException("INVALID_MANGA_ID");

            var dbChapter = _chapters.FirstOrDefault(ch => ch.ChapterId == chapter.ChapterId);

            if (dbChapter == null)
            {
                chapter.CreatedOn = DateTime.Now;

                _chapters.Add(chapter);
                _dbContext.Commit();
                
                return;
            }

            dbChapter = chapter;
            dbChapter.LastUpdatedOn = DateTime.Now;

            _chapters.Update(chapter);
            _dbContext.Commit();
        }

        public void Delete(Guid chapterId)
        {
            var chapterToDelete = _chapters.FirstOrDefault(ch => ch.ChapterId == chapterId)
                ?? throw new ArgumentException("INVALID_CHAPTER_ID");

            _chapters.Remove(chapterToDelete);

            _dbContext.Commit();
        }

        public IQueryable<Chapter> GetAll()
        {
            return _chapters.AsQueryable();
        }

        public Chapter GetById(Guid chapterId)
        {
            var chapter = _chapters.FirstOrDefault(ch => ch.ChapterId == chapterId)
                ?? throw new ArgumentException("INVALID_CHAPTER_ID");

            return chapter;
        }
    }
}