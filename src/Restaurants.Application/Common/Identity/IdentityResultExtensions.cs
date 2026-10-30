using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.Common.Identity
{
    public static class IdentityResultExtensions
    {
        public static void ThrowIfFailed(this IdentityResult result)
        {
            if (result.Succeeded)
                return;

            var errors = string.Join(
                ", ",
                result.Errors.Select(e => e.Description));

            throw new ValidationException(errors);
        }
    }
}