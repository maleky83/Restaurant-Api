using MediatR;

namespace Restaurants.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; set; } = string.Empty;
    }
}
