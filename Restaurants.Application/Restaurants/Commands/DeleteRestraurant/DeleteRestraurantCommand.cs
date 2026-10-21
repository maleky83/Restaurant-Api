using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestraurant;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
