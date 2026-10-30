using MediatR;
using Restaurants.Application.Users.Dtos;

namespace Restaurants.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
