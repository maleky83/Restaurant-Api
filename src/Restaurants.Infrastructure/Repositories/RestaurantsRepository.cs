using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Common.Constants;
using Restaurants.Application.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
            string? searchPhrase,
            int? pageSize,
            int? pageNumber,
            string? sortBy,
            SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Restaurants
                .Where(
                    r => searchPhraseLower == null
                    || r.Name.ToLower().Contains(searchPhraseLower)
                    || r.Description.ToLower().Contains(searchPhraseLower));

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name).ToLower() ,r=>r.Name},
                    {nameof(Restaurant.Description).ToLower(),r=>r.Description },
                    {nameof(Restaurant.Category).ToLower(),r=>r.Category }
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var totalCount = await baseQuery.CountAsync();

            if (pageSize.HasValue && pageNumber.HasValue)
            {
                baseQuery = baseQuery
                    .Skip(pageSize.Value * (pageNumber.Value - 1))
                    .Take(pageSize.GetValueOrDefault());
            }

            var restaurants = await baseQuery
                .Include(r => r.Dishes)
                .ToListAsync();

            return (restaurants, totalCount);
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
}
