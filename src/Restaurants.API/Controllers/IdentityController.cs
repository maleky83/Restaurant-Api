using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Common.Constants;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnassignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;

namespace Restaurants.API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    [Authorize]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
