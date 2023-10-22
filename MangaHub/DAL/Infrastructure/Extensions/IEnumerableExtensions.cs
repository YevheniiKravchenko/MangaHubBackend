using DAL.Infrastructure.Models;

namespace DAL.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IOrderedEnumerable<T> GetPage<T>(this IOrderedEnumerable<T> query, PagingModel model)
        {
            if (model is null)
                return query;

            return (IOrderedEnumerable<T>)query
                .Skip((model.PageCount - 1) * model.PageSize)
                .Take(model.PageSize);
        }
    }
}