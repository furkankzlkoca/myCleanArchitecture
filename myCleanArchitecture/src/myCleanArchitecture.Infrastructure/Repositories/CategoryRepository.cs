

using myCleanArchitecture.Application.Interfaces.Repositories;
using myCleanArchitecture.Infrastructure.Context;
using myCleanArchitecture.Infrastructure.Repositories.Base;

namespace myCleanArchitecture.Infrastructure.Repositories
{
    internal class CategoryRepository : ApplicationBaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
       
    }
}
