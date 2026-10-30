using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService
        ) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(
            UpdateRestaurantCommand request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant {RestaurantId}", request.Id);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            mapper.Map(request, restaurant);

            await restaurantsRepository.SaveChangesAsync();
        }
    }
}
