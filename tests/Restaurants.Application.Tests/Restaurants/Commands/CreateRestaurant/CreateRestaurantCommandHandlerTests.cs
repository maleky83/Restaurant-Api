using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Repositories;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    [Fact]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var validator = new Mock<CreateRestaurantCommandValidator>();

        var userContext = new Mock<IUserContext>();

        var currentUser = new CurrentUser("owner-id", "test@gmail.com", [], null, null);
        userContext.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object,
            mapperMock.Object,
            restaurantRepositoryMock.Object,
            validator.Object,
            userContext.Object);

        // act 
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be(currentUser.Id);
        restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
    }
}
