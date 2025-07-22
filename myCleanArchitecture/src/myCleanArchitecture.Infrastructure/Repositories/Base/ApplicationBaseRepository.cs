using myCleanArchitecture.Infrastructure.Context;


namespace myCleanArchitecture.Infrastructure.Repositories.Base
{
    internal class ApplicationBaseRepository<TEntity> : BaseRepository<TEntity, ApplicationDbContext>
         where TEntity : class, IBaseEntity, new()
    {
        public ApplicationBaseRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
