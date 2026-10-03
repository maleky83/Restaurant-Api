using Restaurants.Domain.Entity;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    public Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
}
