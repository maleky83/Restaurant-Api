using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
    ILogger<CreateDishCommandHandler> logger,
    IMapper mapper,
    IDishesRepository dishRepository,
    IRestaurantsRepository restaurantsRepository,
    IValidator<CreateDishCommand> validator
    ) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating dish {@DishRequest}", request);

        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var restaurant = await restaurantsRepository
            .GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        var dishId = await dishRepository.CreateAsync(dish);
        return dishId;
    }
}
