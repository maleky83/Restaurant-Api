using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandhandler(
        ILogger<UpdateUserCommandhandler> logger,
        UserManager<User> userManager,
        IMapper mapper
        ) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating user for {Id}", request.Id);

            var user = await userManager.FindByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id);

            if (!string.IsNullOrEmpty(request.Password))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await userManager.ResetPasswordAsync(user, token, request.Password);

                passwordResult.ThrowIfFailed();
            }

            if (request.UserRoles is not null)
            {
                var currentRoles = await userManager.GetRolesAsync(user);

                var removeResult =
                    await userManager.RemoveFromRolesAsync(user, currentRoles);

                removeResult.ThrowIfFailed();

                var newRoles = request.UserRoles.Distinct().ToArray();

                if (newRoles.Length > 0)
                {
                    var addResult =
                        await userManager.AddToRolesAsync(user, newRoles);

                    addResult.ThrowIfFailed();
                }
            }

            mapper.Map(request, user);

            var result = await userManager.UpdateAsync(user);
            result.ThrowIfFailed();
        }
    }
}
