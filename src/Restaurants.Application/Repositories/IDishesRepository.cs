using Restaurants.Domain.Entities;

namespace Restaurants.Application.Repositories
{
    public interface IDishesRepository
    {
        Task<int> CreateAsync(Dish entity);
        Task DeleteAllAsync(IEnumerable<Dish> entity);
    }
}
