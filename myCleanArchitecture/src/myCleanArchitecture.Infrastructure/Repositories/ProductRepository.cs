

using myCleanArchitecture.Application.Interfaces.Repositories;
using myCleanArchitecture.Infrastructure.Context;
using myCleanArchitecture.Infrastructure.Identity;
using myCleanArchitecture.Infrastructure.Repositories.Base;

namespace myCleanArchitecture.Infrastructure.Repositories
{
    internal class ProductRepository : ApplicationBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
    
}
