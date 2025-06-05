

using myCleanArchitecture.Application.Interfaces.Repositories;
using myCleanArchitecture.Infrastructure.Context;
using myCleanArchitecture.Infrastructure.Repositories.Base;

namespace myCleanArchitecture.Infrastructure.Repositories
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsNameExist(string name)
        {
            return await AnyAsync(c => c.Name == name && c.Deleted == null);
        }

        public async Task<bool> IsNameExist(string name, Guid id)
        {
            return await AnyAsync(c => c.Name == name && c.Deleted == null && c.Id != id);
        }
    }
}
