using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestraurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService

    ) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(
        DeleteRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await restaurantsRepository.DeleteAsync(restaurant);
    }
}
