using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Constants;
using Restaurants.Infrastructure.Users;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Infrastructure.Tests.Users;

public class UserContextTests
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public UserContextTests()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
    }

    [Fact]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier,"1"),
            new(ClaimTypes.Email,"test@gmail.com"),
            new(ClaimTypes.Role,UserRoles.Admin),
            new(ClaimTypes.Role,UserRoles.User),
            new("Nationality","German"),
            new("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd"))

        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        _httpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(new DefaultHttpContext()
            {
                User = user
            });

        var userContext = new UserContext(_httpContextAccessorMock.Object);

        // act

        var currentUser = userContext.GetCurrentUser();

        // assert

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("1");
        currentUser.Email.Should().Be("test@gmail.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperationException()
    {
        //arrange

        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(_httpContextAccessorMock.Object);

        // act
        Action action = () => userContext.GetCurrentUser();

        // assert

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User context is not present");
    }
}
