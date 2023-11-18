using DAL.Contracts;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            Lazy<IUserRepository> users,
            Lazy<IMangaRepository> mangas,
            Lazy<IChapterRepository> chapters)
        {
            Users = users;
            Mangas = mangas;
            Chapters = chapters;
        }

        public Lazy<IUserRepository> Users { get; }

        public Lazy<IMangaRepository> Mangas { get; }

        public Lazy<IChapterRepository> Chapters { get; }
    }
}