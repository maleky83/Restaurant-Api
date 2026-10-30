using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public string Id { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Nationality { get; set; }
        public string? Password { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IEnumerable<string>? UserRoles { get; set; }
    }
}
