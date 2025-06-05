using Microsoft.EntityFrameworkCore;
using myCleanArchitecture.Application.Interfaces.Repositories.Base;
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Infrastructure.Context;

namespace myCleanArchitecture.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task SaveChangesAsync()
        {
            TrackingEntitesChanges();
            await _context.SaveChangesAsync();
        }
        private void TrackingEntitesChanges()
        {
            var entities = _context.ChangeTracker.Entries<IDetailedBaseEntity>()
                .Where(x => x.State != EntityState.Unchanged);

            if (!entities.Any())
                return;

            DateTime now = DateTime.Now;
            string userId = _currentUserService.GetCurrentUserDto().UserId;
            foreach (var item in entities)
            {
                IDetailedBaseEntity detailedBaseEntity = item.Entity;
                if (detailedBaseEntity is null) continue;

                if (item.State == EntityState.Added)
                {
                    detailedBaseEntity.Created = now;
                    detailedBaseEntity.CreatedBy = userId;
                }
                else if (item.State == EntityState.Modified)
                {
                    detailedBaseEntity.LastModified = now;
                    detailedBaseEntity.LastModifiedBy = userId;
                }
                else if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified; // Change state to Modified to avoid actual deletion
                    detailedBaseEntity.Deleted = now;
                    detailedBaseEntity.DeletedBy = userId;
                }
            }
        }

        //IBaseRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                var repository = new BaseRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return (IBaseRepository<TEntity>)_repositories[type];
        }
    }
}
