using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using myCleanArchitecture.Application.Interfaces.Repositories.Base;
using myCleanArchitecture.Infrastructure.Context;
using myCleanArchitecture.Shared.Results;
using System.Linq;
using System.Linq.Expressions;

namespace myCleanArchitecture.Infrastructure.Repositories.Base
{
    internal class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
        where TEntity : class, IBaseEntity, new()
        where TContext : DbContext
    {
        
        protected readonly TContext _context;
        public BaseRepository(TContext context)
        {
            _context = context;
        }
        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AnyAsync(filter);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        {
            return await include(_context.Set<TEntity>()).FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity?> GetByIdAsync<T>(T id) where T : struct
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>> orderBy, bool isDesc)
        {
            var list = filter == null ? _context.Set<TEntity>().AsNoTracking()
                                    : _context.Set<TEntity>().AsNoTracking().Where(filter);

            list = isDesc ? list.OrderByDescending(orderBy) : list.OrderBy(orderBy);
            return await list.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>> orderBy, bool isDesc, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        {
            var list = filter == null ? _context.Set<TEntity>().AsNoTracking()
                                    : _context.Set<TEntity>().AsNoTracking().Where(filter);
            
            list = include(list);

            list = isDesc ? list.OrderByDescending(orderBy) : list.OrderBy(orderBy);
            return await list.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsyncNoTracking(Expression<Func<TEntity, bool>> filter)
        {
            var list = filter == null ? _context.Set<TEntity>()
                                  : _context.Set<TEntity>().Where(filter);
            return await list.ToListAsync();
        }

        public async Task<PagingResult<TEntity>> GetPagingResultAsync(PagingParameters pagingParameters, Expression<Func<TEntity, bool>>? filter)
        {
            if (pagingParameters.Page < 1) pagingParameters.Page = 1;
            if (pagingParameters.PageSize < -1) pagingParameters.PageSize = 10;

            var query = _context.Set<TEntity>().AsNoTracking();
            if (filter is not null)
                query = query.Where(filter);

            int totalCount = await query.CountAsync();
            if (pagingParameters.OrderBy is not null)
            {
                query = pagingParameters.IsDesc
                    ? query.OrderByDescending(ToLambda<TEntity>(pagingParameters.OrderBy))
                    : query.OrderBy(ToLambda<TEntity>(pagingParameters.OrderBy));
            }

            var pagingResult = new PagingResult<TEntity>();
            if (pagingParameters.PageSize == -1)
            {
                pagingParameters.PageSize = totalCount;
                pagingResult.Entities = await query.ToListAsync();
            }
            else
            {
                pagingResult.Entities = await query.Skip((pagingParameters.Page - 1) * pagingParameters.PageSize)
                                                   .Take(pagingParameters.PageSize)
                                                   .ToListAsync();
            }
            pagingResult.PagingParameters = new PagingParameters
            {
                TotalCount = totalCount,
                TotalPages = pagingParameters.PageSize > 0 ? (int)Math.Ceiling((double)totalCount / pagingParameters.PageSize) : 1,
                PageSize = pagingParameters.PageSize,
                OrderBy = pagingParameters.OrderBy,
                Page = pagingParameters.Page,
                CurrentPageSize = pagingResult.Entities.Count
            };
            return pagingResult;
        }
        private static Expression<Func<TEntity, object>> ToLambda<TEntity>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            //var property = Expression.Property(parameter, propertyName);
            var property = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
            var convert = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(convert, parameter);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public List<TEntity> UpdateRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }
    }
}
