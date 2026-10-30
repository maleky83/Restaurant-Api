using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> CreateAsync(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAllAsync(IEnumerable<Dish> entity)
        {
            dbContext.Dishes.RemoveRange(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
