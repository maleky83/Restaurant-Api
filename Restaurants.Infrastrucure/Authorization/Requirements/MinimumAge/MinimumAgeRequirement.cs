using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastrucure.Authorization.Requirements.MinimumAge;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
