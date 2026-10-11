using Microsoft.AspNetCore.Http;
using Restaurants.Application.User;
using System.Security.Claims;

namespace Restaurants.Infrastrucure.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user is null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);

        return new CurrentUser(userId, email, roles);
    }
}
