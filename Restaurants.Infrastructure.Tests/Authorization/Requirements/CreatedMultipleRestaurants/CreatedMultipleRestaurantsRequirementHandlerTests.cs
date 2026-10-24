using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;

    public CreatedMultipleRestaurantsRequirementHandlerTests()
    {
        _userContextMock = new Mock<IUserContext>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
    }

    [Fact]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        // arragne
        var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);

        _userContextMock
            .Setup(r => r.GetCurrentUser())
            .Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId=currentUser.Id,
            },
            new()
            {
                OwnerId=currentUser.Id,
            },
            new()
            {
                OwnerId="2"
            },
        };

        _restaurantsRepositoryMock
           .Setup(r => r.GetAllAsync())
           .ReturnsAsync(restaurants);

        var requirement = new CreatedMultipleRestaurantsRequirement(2);

        var context = new AuthorizationHandlerContext([requirement], null!, null);

        var handle = new CreatedMultipleRestaurantsRequirementHandler(
            _userContextMock.Object,
            _restaurantsRepositoryMock.Object);

        // act
        await handle.HandleAsync(context);

        // assert
        context.HasSucceeded.Should().BeTrue();

    }
}
