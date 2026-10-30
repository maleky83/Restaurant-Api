using MediatR;
using Restaurants.Application.Users.Dtos;

namespace Restaurants.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery(string id) : IRequest<UserDto>
    {
        public string Id { get; } = id;
    }
}
