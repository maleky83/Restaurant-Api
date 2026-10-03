using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entity;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastrucure.Persistence;

namespace Restaurants.Infrastrucure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();

        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext
            .Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant;
    }
}
