using FluentValidation;
using Restaurants.Application.Common.Constants;

namespace Restaurants.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly List<string> Roles = [UserRoles.Admin, UserRoles.Owner, UserRoles.User];
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty();

            RuleFor(u => u.UserRoles)
                .Must(roles =>
                    roles is null ||
                    (roles.Any() && roles.All(role => Roles.Contains(role))))
                .WithMessage("One or more roles are invalid.");
        }
    }
}
