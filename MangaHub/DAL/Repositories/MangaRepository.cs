using DAL.Contracts;
using DAL.DbContexts;
using DAL.Infrastructure.Extensions;
using DAL.Infrastructure.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly DbContextBase _dbContext;

        private readonly DbSet<Manga> _mangas;
        private readonly DbSet<Rating> _ratings;
        private readonly DbSet<User> _users;

        public MangaRepository(DbContextBase dbContext)
        {
            _dbContext = dbContext;

            _mangas = dbContext.Mangas;
            _ratings = dbContext.Ratings;
            _users = dbContext.Users;
        }

        public void Apply(Manga manga)
        {
            var dbManga = _mangas.FirstOrDefault(m => m.MangaId == manga.MangaId);

            if (dbManga == null)
            {
                manga.CreatedOn = DateTime.Now;
                _mangas.Add(manga);
                
                _dbContext.Commit();
                return;
            }

            dbManga = manga;
            dbManga.LastUpdatedOn = DateTime.Now;

            _mangas.Update(dbManga);
            _dbContext.Commit();
        }

        public void Delete(Guid mangaId)
        {
            var mangaToDelete = _mangas.FirstOrDefault(m => m.MangaId == mangaId)
                ?? throw new ArgumentException("INVALID_MANGA_ID");

            _mangas.Remove(mangaToDelete);

            _dbContext.Commit();
        }

        public IQueryable<Manga> GetAll()
        {
            return _mangas.AsQueryable()
                .Include(m => m.Ratings);
        }

        public IQueryable<Manga> GetAll(MangaFilter filter)
        {
            var mangas = filter.Filter(_mangas)
                .OrderByDescending(m => m.ReleasedOn)
                .GetPage(filter.PagingModel)
                .Include(m => m.Ratings);

            return mangas;
        }

        public Manga GetById(Guid mangaId)
        {
            var manga = _mangas.Include(m => m.User)
                .Include(m => m.Ratings)
                .Include(m => m.Chapters)
                .FirstOrDefault(m => m.MangaId == mangaId)
                    ?? throw new ArgumentException("INVALID_MANGA_ID");

            return manga;
        }

        public void SetRating(Guid mangaId, int userId, int mark)
        {
            var manga = _mangas.FirstOrDefault(m => m.MangaId == mangaId)
                ?? throw new ArgumentException("INVALID_MANGA_ID");

            var user = _users.FirstOrDefault(u => u.UserId == userId)
                ?? throw new ArgumentException("INVALID_USER_ID");

            var rating = _ratings.FirstOrDefault(r => r.MangaId == mangaId
                && r.UserId.HasValue
                && r.UserId.Value == userId);
                    
            if (rating == null)
            {
                rating = new Rating
                {
                    MangaId = mangaId,
                    UserId = userId,
                };

                _ratings.Add(rating);
            }

            rating.Mark = (byte)mark;

            _dbContext.Commit();
        }

        public void UploadCoverImage(byte[] imageBytes, Guid mangaId)
        {
            var manga = GetById(mangaId);

            manga.CoverImage = imageBytes;
            manga.LastUpdatedOn = DateTime.Now;

            _mangas.Update(manga);
            _dbContext.Commit();
        }
    }
}
