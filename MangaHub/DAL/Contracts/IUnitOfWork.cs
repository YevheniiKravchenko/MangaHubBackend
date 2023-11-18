namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Lazy<IUserRepository> Users { get; }

        Lazy<IMangaRepository> Mangas { get; }

        Lazy<IChapterRepository> Chapters { get; }
    }
}
