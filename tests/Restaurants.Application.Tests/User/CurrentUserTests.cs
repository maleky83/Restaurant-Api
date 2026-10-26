using FluentAssertions;
using Restaurants.Application.Constants;
using Restaurants.Application.Users;
using Xunit;

namespace Restaurants.Application.Tests.User;

public class CurrentUserTests
{
    //TestMethod_Scenario_ExpectResult
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string userRole)
    {
        //arrange
        var currentUser = new CurrentUser("1",
            "test@gmail.com",
            [UserRoles.Admin, UserRoles.User],
            null,
            null);

        // act
        var inIsRole = currentUser.IsInRole(userRole);

        //assert
        inIsRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
    {
        var currentUser = new CurrentUser("2",
            "test2@gmail.com",
            [UserRoles.Admin, UserRoles.User],
            null,
            null);

        var inIsRole = currentUser.IsInRole(UserRoles.Owner);

        inIsRole.Should().BeFalse();
    }

    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.Owner)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithNotMatchingRoleCase_ShouldReturnFalse(string userRole)
    {
        var currentUser = new CurrentUser("3",
            "test3@gmail.com",
            [UserRoles.Admin, UserRoles.User],
            null,
            null);

        var inIsRole = currentUser.IsInRole(userRole.ToLower());

        inIsRole.Should().BeFalse();
    }
}
