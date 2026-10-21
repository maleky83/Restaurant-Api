using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastrucure.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated) :
    IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;
}
