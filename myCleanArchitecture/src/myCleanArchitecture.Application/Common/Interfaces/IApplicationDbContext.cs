
using Microsoft.EntityFrameworkCore;
using myCleanArchitecture.Domain.Models;

namespace myCleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Category { get;  }
        DbSet<Product> Product { get;  }
    }
}
