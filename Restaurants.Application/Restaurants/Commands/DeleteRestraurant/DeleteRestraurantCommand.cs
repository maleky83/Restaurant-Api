using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestraurant;

public class DeleteRestraurantCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
