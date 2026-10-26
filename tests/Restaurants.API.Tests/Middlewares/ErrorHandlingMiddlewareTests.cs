using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;


namespace Restaurants.API.Tests.Middlewares;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
    private readonly DefaultHttpContext _context;
    private readonly Mock<RequestDelegate> _nextDelegateMock;
    private readonly ErrorHandlingMiddleware _middleware;
    public ErrorHandlingMiddlewareTests()
    {
        _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        _context = new DefaultHttpContext();
        _nextDelegateMock = new Mock<RequestDelegate>();
        _middleware = new ErrorHandlingMiddleware(_loggerMock.Object);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        // act
        await _middleware.InvokeAsync(_context, _nextDelegateMock.Object);

        // assert
        _nextDelegateMock.Verify(next => next.Invoke(_context), Times.Once);

    }

    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
    {
        // arrange
        var notFoundException = new NotFoundException(nameof(Restaurant), "1");

        //act
        await _middleware.InvokeAsync(_context, _ => throw notFoundException);

        // assert
        _context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
    {
        // Arrange
        var exception = new ForbidException();
        // Act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        // Assert
        _context.Response.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task InvokeAsync_WhenValidationExceptionThrown_ShouldSetStatusCode400()
    {
        // Arrange
        var exception = new ValidationException("Error validation exception");
        // Act
        await _middleware
            .InvokeAsync(_context, _ => throw exception);

        // Assert
        _context.Response.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task InvokeAsync_WhenExceptionThrown_ShouldSetStatusCode500()
    {
        // Arrange
        var exception = new Exception();

        // Act
        await _middleware.InvokeAsync(_context, _ => throw exception);

        // Assert
        _context.Response.StatusCode.Should().Be(500);
    }
}
