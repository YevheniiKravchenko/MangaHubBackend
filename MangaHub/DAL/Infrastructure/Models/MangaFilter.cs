using Common.Extensions;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure.Models
{
    public class MangaFilter : FilterBase<Manga>
    {
        public string SearchQuery { get; set; }

        public Genre? Genre { get; set; }

        public DateTime? ReleasedOn { get; set; }

        public double? Rating { get; set; }

        public override IQueryable<Manga> Filter(DbSet<Manga> mangas)
        {
            var query = mangas.AsQueryable();

            if (!SearchQuery.IsNullOrEmpty())
            {
                query = query.Where(m => m.Title.StartsWith(SearchQuery)
                    || m.User.UserProfile.FirstName.StartsWith(SearchQuery)
                    || m.User.UserProfile.LastName.StartsWith(SearchQuery));
            }

            if (Genre.HasValue)
                query = query.Where(m => m.Genre == (int)Genre);

            if (ReleasedOn.HasValue)
                query = query.Where(m => m.ReleasedOn.Year == ReleasedOn.Value.Year);

            if (Rating.HasValue)
                query = query.Where(m => m.Ratings.Average(r => r.Mark) >= Rating.Value);

            return query;
        }
    }
}
