using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastrucure.Persistence;
using Restaurants.Infrastrucure.Repositories;
using Restaurants.Infrastrucure.Seeders;
using Restaurants.Infrastrucure.Users;

namespace Restaurants.Infrastrucure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastrucure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");

        services.AddDbContext<RestaurantsDbContext>(options => options
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
        );

        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();
    }
}
