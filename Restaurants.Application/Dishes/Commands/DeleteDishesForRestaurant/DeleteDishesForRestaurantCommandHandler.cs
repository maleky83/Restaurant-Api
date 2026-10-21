using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommandHandler(
    ILogger<DeleteDishesForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService
    ) :
    IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Deleting all dishes for restaurant {RestaurantId}",
            request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await dishesRepository.DeleteAllAsync(restaurant.Dishes);
    }
}
