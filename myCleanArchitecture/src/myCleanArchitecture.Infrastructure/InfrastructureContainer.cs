using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myCleanArchitecture.Application.Common.Interfaces;
using myCleanArchitecture.Infrastructure.Data.Context;
using myCleanArchitecture.Infrastructure.Identity;

namespace myCleanArchitecture.Infrastructure
{
    public static class InfrastructureContainer
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); // Assuming a "DefaultConnection" in appsettings.json

        }
    }
}
