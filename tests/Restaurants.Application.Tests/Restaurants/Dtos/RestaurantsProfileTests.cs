using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Dtos;

public class RestaurantsProfileTests : Profile
{
    private readonly IMapper _mapper;
    public RestaurantsProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
    {
        // arrange

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "test name",
            Description = "test description",
            Category = "test category",
            HasDelivery = true,
            ContactEmail = "test@gmail.com",
            ContactNumber = "123456789",
            Address = new Address
            {
                City = "test city",
                Street = "test street",
                PostalCode = "12345",
            }
        };

        // act 
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        // assert
        restaurantDto.Should().NotBeNull();
        restaurantDto.Id.Should().Be(restaurant.Id);
        restaurantDto.Name.Should().Be(restaurant.Name);
        restaurantDto.Description.Should().Be(restaurant.Description);
        restaurantDto.Category.Should().Be(restaurant.Category);
        restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
        restaurantDto.City.Should().Be(restaurant.Address.City);
        restaurantDto.Street.Should().Be(restaurant.Address.Street);
        restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        //arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "test",
            Description = "test",
            HasDelivery = true,
        };

        // act
        var restaurant = _mapper.Map<Restaurant>(command);

        // assert
        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(command.Id);
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
    }

    [Fact]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        // arrange
        var command = new CreateRestaurantCommand
        {
            Name = "test",
            Description = "test",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "test@gmail.com",
            ContactNumber = "123456789",
            City = "test",
            Street = "test",
            PostalCode = "12345",
        };

        // act
        var restaurant = _mapper.Map<Restaurant>(command);

        // assert
        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.ContactEmail.Should().Be(command.ContactEmail);
        restaurant.ContactNumber.Should().Be(command.ContactNumber);
        restaurant.Category.Should().Be(command.Category);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);
        restaurant.Address?.City.Should().Be(command.City);
        restaurant.Address?.Street.Should().Be(command.Street);
        restaurant.Address?.PostalCode.Should().Be(command.PostalCode);
    }
}
