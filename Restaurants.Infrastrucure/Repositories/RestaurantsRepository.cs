using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Infrastrucure.Persistence;

namespace Restaurants.Infrastrucure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext
            .Restaurants
            .Include(r => r.Dishes)
            .ToListAsync();

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

    public async Task<int> CreateAsync(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;

    }

    public async Task DeleteAsync(Restaurant entity)
    {
        dbContext.Restaurants.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}
