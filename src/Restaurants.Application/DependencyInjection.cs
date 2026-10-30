using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

            services.AddAutoMapper(cft => { }, applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly);
        }
    }
}
