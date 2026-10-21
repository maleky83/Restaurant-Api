using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Infrastrucure.Authorization;
using Restaurants.Infrastrucure.Authorization.Requirements.CreatedMultipleRestaurants;
using Restaurants.Infrastrucure.Authorization.Requirements.MinimumAge;
using Restaurants.Infrastrucure.Authorization.Services;
using Restaurants.Infrastrucure.Constants;
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
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        services.AddScoped<IUserContext, UserContext>();

        services.AddHttpContextAccessor();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "iran"))
            .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
            .AddPolicy(PolicyNames.CreatedAtleast2Restaurants,
                builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();

        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
    }
}
