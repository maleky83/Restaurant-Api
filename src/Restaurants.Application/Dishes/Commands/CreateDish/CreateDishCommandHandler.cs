using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common.Constants;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> logger,
        IMapper mapper,
        IDishesRepository dishRepository,
        IRestaurantsRepository restaurantsRepository,
        IValidator<CreateDishCommand> validator,
        IRestaurantAuthorizationService restaurantAuthorizationService
        ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating dish {@DishRequest}", request);

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var restaurant = await restaurantsRepository
                .GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            {
                throw new ForbidException();
            }

            var dish = mapper.Map<Dish>(request);

            var dishId = await dishRepository.CreateAsync(dish);
            return dishId;
        }
    }
}
