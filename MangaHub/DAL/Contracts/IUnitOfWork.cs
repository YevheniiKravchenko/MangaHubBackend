namespace DAL.Contracts
{
    public interface IUnitOfWork
    {
        Lazy<IUserRepository> Users { get; }
    }
}
