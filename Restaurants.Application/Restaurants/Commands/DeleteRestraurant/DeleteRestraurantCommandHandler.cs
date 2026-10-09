using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestraurant;

public class DeleteRestraurantCommandHandler(
    ILogger<DeleteRestraurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository

    ) : IRequestHandler<DeleteRestraurantCommand, bool>
{
    public async Task<bool> Handle(
        DeleteRestraurantCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
            return false;

        await restaurantsRepository.DeleteAsync(restaurant);
        return true;
    }
}
