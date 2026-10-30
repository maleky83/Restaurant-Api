using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IValidator<CreateRestaurantCommand> validator,
        IUserContext userContext
        ) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(
            CreateRestaurantCommand request,
            CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation(
                "{UserEmail} [{UserId}] is Creating a new restaurant {@Restaurant}",
                currentUser.Email,
                currentUser.Id,
                request
                );

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var restaurant = mapper.Map<Restaurant>(request);

            restaurant.OwnerId = currentUser.Id;

            int id = await restaurantsRepository.CreateAsync(restaurant);

            return id;
        }
    }
}
