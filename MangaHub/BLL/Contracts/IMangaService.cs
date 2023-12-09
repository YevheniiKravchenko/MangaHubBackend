using BLL.Infrastructure.Models;
using DAL.Infrastructure.Models;
using Domain.Models;

namespace BLL.Contracts
{
    public interface IMangaService
    {
        IEnumerable<MangaListItem> GetAll();

        IEnumerable<MangaListItem> GetAll(MangaFilter filter);

        MangaModel GetById(Guid mangaId, int currentUserId);

        void Apply(MangaModel mangaModel);

        void Delete(Guid mangaId);

        void SetRating(Guid mangaId, int userId, int mark);

        void UploadCoverImage(byte[] imageBytes, Guid mangaId);
    }
}