using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler(
        ILogger<GetDishesForRestaurantQueryHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository
        ) :
        IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for restaurant {RestaurantId}",
                request.RestaurantId);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

            return result;
        }
    }
}
