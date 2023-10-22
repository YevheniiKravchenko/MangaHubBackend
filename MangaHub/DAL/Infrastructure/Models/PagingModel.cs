namespace DAL.Infrastructure.Models
{
    public class PagingModel
    {
        public int PageSize { get; set; } = int.MaxValue;

        public int PageCount { get; set; } = 1;
    }
}
