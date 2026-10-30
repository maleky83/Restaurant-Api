using Restaurants.Application.Common.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}
