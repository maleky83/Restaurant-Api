using MediatR;

namespace Restaurants.Application.Users.Commands.DeleteUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
