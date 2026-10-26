using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Repositories;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization.Requirements.CreatedMultipleRestaurants;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Authorization.Requirements.CreatedMultipleRestaurants;

public class CreatedMultipleRestaurantsRequirementHandlerTests
{
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly CurrentUser _currentUser;

    public CreatedMultipleRestaurantsRequirementHandlerTests()
    {
        _userContextMock = new Mock<IUserContext>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        _currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
    }

    [Fact]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        // arragne

        _userContextMock
            .Setup(r => r.GetCurrentUser())
            .Returns(_currentUser);

        var restaurants = new List<Restaurant>
        {
            new() { OwnerId = _currentUser.Id },
            new() { OwnerId = _currentUser.Id },
            new() { OwnerId = _currentUser.Id },
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
        context.HasFailed.Should().BeFalse();
    }

    [Fact]
    public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
    {
        // arrange

        _userContextMock.Setup(m => m.GetCurrentUser()).Returns(_currentUser);

        var restaurants = new List<Restaurant>()
        {
            new(){OwnerId="3" },
            new(){OwnerId="3" },
            new(){OwnerId=_currentUser.Id },
        };

        _restaurantsRepositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);

        var requirement = new CreatedMultipleRestaurantsRequirement(2);

        var context = new AuthorizationHandlerContext([requirement], null!, null);

        var handle = new CreatedMultipleRestaurantsRequirementHandler(
            _userContextMock.Object, _restaurantsRepositoryMock.Object);

        // act
        await handle.HandleAsync(context);

        // assert

        context.HasFailed.Should().BeTrue();
        context.HasSucceeded.Should().BeFalse();
    }
}
