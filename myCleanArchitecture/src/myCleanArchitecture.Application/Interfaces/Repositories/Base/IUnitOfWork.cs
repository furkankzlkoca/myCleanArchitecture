

using myCleanArchitecture.Domain.Common;

namespace myCleanArchitecture.Application.Interfaces.Repositories.Base
{
    public interface IUnitOfWork
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity, new();
        Task SaveChangesAsync();
    }
}
