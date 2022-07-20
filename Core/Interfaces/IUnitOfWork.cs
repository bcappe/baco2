using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
         
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete(); //cuida quantas coisas mudaram
    }
    }
}