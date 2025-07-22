
using System.Linq.Expressions;

namespace myCleanArchitecture.Application.Interfaces.Repositories.Base
{
    public interface IExternalBaseEntity
    {

    }
    public interface IExternalBaseRepository<TEntity, TContext> : IExternalBaseRepository<TEntity>
        where TEntity : IExternalBaseEntity
        where TContext : DbContext
    {

    }
    public interface IExternalBaseRepository<TEntity> where TEntity : IExternalBaseEntity
    {
        Task<TEntity> GetByIdAsync<T>(T id) where T : struct;
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>> orderBy, bool isDesc);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}
