
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace myCleanArchitecture.Application.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : IBaseEntity
        where TContext : DbContext
    {

    }
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity?> GetByIdAsync<T>(T id) where T : struct;
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include);
        TEntity Add(TEntity entity);
        Task<List<TEntity>> AddRangeAsync(List<TEntity> entities);
        TEntity Update(TEntity entity);
        List<TEntity> UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>> orderBy, bool isDesc);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>> orderBy, bool isDesc, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include);
        Task<IEnumerable<TEntity>> GetListAsyncNoTracking(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<PagingResult<TEntity>> GetPagingResultAsync(PagingParameters pagingParameters, Expression<Func<TEntity, bool>>? filter);
    }    
}
