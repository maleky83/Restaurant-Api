using MediatR;

namespace Restaurants.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Nationality { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IEnumerable<string> UserRoles { get; set; } = [];
    }
}
