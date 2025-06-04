

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using myCleanArchitecture.Application.Common.Behaviours;
using System.Reflection;

namespace myCleanArchitecture.Application
{
    public static class ApplicationContainer
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // TODO: Uncomment and implement these behaviors as needed
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
        }
    }
}
