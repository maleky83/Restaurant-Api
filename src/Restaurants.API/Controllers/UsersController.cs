using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Common.Constants;
using Restaurants.Application.Users.Commands.CreateUser;
using Restaurants.Application.Users.Commands.DeleteUser;
using Restaurants.Application.Users.Commands.UpdateUser;
using Restaurants.Application.Users.Queries.GetAllUsers;
using Restaurants.Application.Users.Queries.GetUserById;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await mediator.Send(command);
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser([FromRoute] string id,
            [FromBody] UpdateUserCommand command)
        {
            command.Id = id;

            await mediator.Send(command);

            return NoContent();
        }
    }
}
