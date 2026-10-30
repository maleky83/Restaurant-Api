using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(
        ILogger<GetAllUsersQueryHandler> logger,
        UserManager<User> userManager,
        IMapper mapper
        ) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all users");

            var users = await userManager.Users.ToListAsync();
            var userDtos = mapper.Map<List<UserDto>>(users);

            for (var i = 0; i < users.Count; i++)
            {
                var roles = await userManager.GetRolesAsync(users[i]);
                userDtos[i].Roles = roles;
            }

            return userDtos;
        }
    }
}
