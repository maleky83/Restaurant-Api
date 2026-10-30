using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common.Identity;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        ILogger<CreateUserCommandHandler> logger,
        UserManager<User> userManager,
        IMapper mapper,
        IValidator<CreateUserCommand> validator
        ) : IRequestHandler<CreateUserCommand>
    {
        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating user");

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var email = await userManager.FindByEmailAsync(request.Email);

            if (email != null)
            {
                throw new ValidationException("Email exists");
            }

            var user = mapper.Map<User>(request);

            var result = await userManager.CreateAsync(user, request.Password);

            result.ThrowIfFailed();

            if (request.UserRoles.Any())
            {
                var roleResult = await userManager.AddToRolesAsync(user, request.UserRoles);

                roleResult.ThrowIfFailed();
            }
        }
    }
}
