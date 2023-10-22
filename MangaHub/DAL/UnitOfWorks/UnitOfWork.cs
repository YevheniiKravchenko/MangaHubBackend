using DAL.Contracts;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(Lazy<IUserRepository> users)
        {
            Users = users;
        }

        public Lazy<IUserRepository> Users { get; }
    }
}