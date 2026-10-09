using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entity;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(
    ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IValidator<CreateRestraurantCommand> validator
    ) : IRequestHandler<CreateRestraurantCommand, int>
{
    public async Task<int> Handle(
        CreateRestraurantCommand request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        logger.LogInformation("Creating a new restaurant {@Restaurant}", request);

        var restaurant = mapper.Map<Restaurant>(request);

        int id = await restaurantsRepository.CreateAsync(restaurant);

        return id;
    }
}
