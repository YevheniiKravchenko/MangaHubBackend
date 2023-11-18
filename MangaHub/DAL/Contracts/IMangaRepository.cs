using DAL.Infrastructure.Models;
using Domain.Models;

namespace DAL.Contracts
{
    public interface IMangaRepository
    {
        IQueryable<Manga> GetAll();

        IQueryable<Manga> GetAll(MangaFilter filter);

        Manga GetById(Guid mangaId);

        void Apply(Manga manga);

        void Delete(Guid mangaId);

        void SetRating(Guid mangaId, int userId, int mark);
    }
}
