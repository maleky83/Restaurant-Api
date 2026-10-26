using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;

internal class CreatedMultipleRestaurantsRequirementHandler(
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository
    ) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CreatedMultipleRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        if (user == null)
        {
            context.Fail();
            return;
        }

        var restaurants = await restaurantsRepository.GetAllAsync();

        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == user.Id);

        if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
