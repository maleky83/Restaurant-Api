using FluentValidation;
using Microsoft.AspNetCore.Http;
using Restaurants.Application.Users;
using Restaurants.Infrastructure.Constants;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Users
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;

            if (user is null)
            {
                throw new ValidationException("User context is not present");
            }

            var claims = user.Claims.Select(c => new { c.Type, c.Value }).ToList();

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(ClaimTypes.Email)!.Value;
            var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
            var nationality = user.FindFirst(AppClaimTypes.Nationality)?.Value;
            var sfd = user.FindFirst(c => c.Type == AppClaimTypes.DateOfBirth);
            var dateOfBirthString = user.FindFirst(AppClaimTypes.DateOfBirth)?.Value;
            var dateOfBirth = dateOfBirthString == null
                ? (DateOnly?)null
                : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

            return new CurrentUser(userId, email, roles, nationality, dateOfBirth);
        }
    }
}
