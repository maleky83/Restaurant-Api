using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestraurant;

public class DeleteRestraurantCommandHandler(
    ILogger<DeleteRestraurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository

    ) : IRequestHandler<DeleteRestraurantCommand>
{
    public async Task Handle(
        DeleteRestraurantCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        await restaurantsRepository.DeleteAsync(restaurant);
    }
}
