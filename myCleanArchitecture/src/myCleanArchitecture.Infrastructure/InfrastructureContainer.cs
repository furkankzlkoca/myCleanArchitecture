using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myCleanArchitecture.Application.Interfaces;
using myCleanArchitecture.Application.Interfaces.Repositories;
using myCleanArchitecture.Application.Interfaces.Repositories.Base;
using myCleanArchitecture.Application.Interfaces.Services;
using myCleanArchitecture.Infrastructure.Context;
using myCleanArchitecture.Infrastructure.Identity;
using myCleanArchitecture.Infrastructure.Repositories;
using myCleanArchitecture.Infrastructure.Repositories.Base;
using myCleanArchitecture.Infrastructure.Services;

namespace myCleanArchitecture.Infrastructure
{
    public static class InfrastructureContainer
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); // Assuming a "DefaultConnection" in appsettings.json
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            // TODO: maybe IApplicationDbContext injection remove

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
