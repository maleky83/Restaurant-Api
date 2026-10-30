using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        UserManager<User> userManager
        ) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deletign user : {UserId}", request.Id);

            var user = await userManager.FindByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(User), request.Id);

            var result = await userManager.DeleteAsync(user);
        }
    }
}
