using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper
    ) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(
        UpdateRestaurantCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant {RestaurantId}", request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        mapper.Map(request, restaurant);

        await restaurantsRepository.SaveChangesAsync();
    }
}
