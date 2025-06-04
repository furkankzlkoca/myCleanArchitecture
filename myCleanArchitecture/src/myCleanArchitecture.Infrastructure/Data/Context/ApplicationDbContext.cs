using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myCleanArchitecture.Application.Common.Interfaces;
using myCleanArchitecture.Infrastructure.Identity;
using System.Reflection;

namespace myCleanArchitecture.Infrastructure.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Category => Set<Category>();

        public DbSet<Product> Product => Set<Product>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ApplicationDbInitializer.SeedData(builder);
        }
    }
}
