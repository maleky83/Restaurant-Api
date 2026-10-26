using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application.Repositories;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Restaurants.API.Tests.Controllers;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepository = new();
    private readonly int _id;
    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services
                    .Replace(ServiceDescriptor
                        .Scoped(typeof(IRestaurantsRepository), _ => _restaurantsRepository.Object));
            });
        });

        _client = _factory.CreateClient();
        _id = 99;
    }

    [Fact]
    public async Task GetAll_ForValidRequest_Return200Ok()
    {
        // Arrange
        var restaurants = new List<Restaurant>();

        _restaurantsRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(restaurants);

        // Act
        var result = await _client
            .GetAsync("/api/restaurants?PageSize=5&SearchPhrase=n&PageNumber=2", CancellationToken.None);

        // Assert 
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_ForInvalidRequest_Return400BadRequest()
    {
        // Arrange
        var restaurants = new List<Restaurant>();

        _restaurantsRepository.Setup(r => r.GetAllAsync());

        // Act
        var result = await _client
            .GetAsync("/api/restaurants?PageSize=3&PageNumber=-1", CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        // Arrange
        _restaurantsRepository.Setup(r => r.GetByIdAsync(_id))
            .ReturnsAsync((Restaurant?)null);

        // Act
        var response = await _client.GetAsync($"/api/restaurants/{_id}", CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        // Arrange
        var restaurant = new Restaurant()
        {
            Id = _id,
            Name = "test",
            Description = "test",
        };

        _restaurantsRepository.Setup(r => r.GetByIdAsync(_id))
            .ReturnsAsync(restaurant);

        // Act
        var response = await _client.GetAsync($"/api/restaurants/{_id}", CancellationToken.None);
        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>(CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be("test");
        restaurantDto.Description.Should().Be("test");
        restaurantDto.Id.Should().Be(_id);
    }
}
