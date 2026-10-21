using Restaurants.Domain.Entities;

namespace Restaurants.Application.Repositories;

public interface IRestaurantsRepository
{
    Task<int> CreateAsync(Restaurant entity);
    Task DeleteAsync(Restaurant entity);
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
