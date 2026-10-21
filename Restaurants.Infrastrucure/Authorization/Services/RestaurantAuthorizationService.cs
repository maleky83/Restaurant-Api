using Microsoft.Extensions.Logging;
using Restaurants.Application.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastrucure.Authorization.Services;

public class RestaurantAuthorizationService(
    ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext
    ) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation(
            "Authorization user {UserEmail} , to {Operation} for restaurant {RestaurantName}",
            user.Email,
            resourceOperation,
            restaurant.Name);

        if (
            resourceOperation == ResourceOperation.Create ||
            resourceOperation == ResourceOperation.Read)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (
            resourceOperation == ResourceOperation.Delete &&
            user.IsInRole(UserRoles.Admin)
           )
        {
            logger.LogInformation(
                "Admin user , delete operation - successful authorization");

            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete ||
            resourceOperation == ResourceOperation.Update) &&
            user.Id == restaurant.OwnerId
          )
        {
            logger.LogInformation("Restaurant owner - successful authorization");

            return true;
        }

        return false;
    }
}
