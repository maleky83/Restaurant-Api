using Restaurants.Domain.Entities;
using Restaurants.Infrastrucure.Persistence;

namespace Restaurants.Infrastrucure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (!dbContext.Restaurants.Any())
        {
            var restaurants = GetRestaurants();
            dbContext.Restaurants.AddRange(restaurants);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants =
    [
        new()
        {
            Name = "KFC",
            Category = "Fast Food",
            Description = "KFC is an American fast food restaurant chain.",
            ContactEmail = "contact@kfc.com",
            HasDelivery = true,

            Dishes =
            [
                new()
                {
                    Name = "Nashville Hot Chicken",
                    Description = "Nashville Hot Chicken (10 pcs.)",
                    Price = 10.30M
                },

                new()
                {
                    Name = "Chicken Nuggets",
                    Description = "Chicken Nuggets (5 pcs.)",
                    Price = 5.30M
                }
            ],
            Address = new ()
            {
                City = "London",
                Street = "Cork St 5",
                PostalCode = "WC2N 5DI",
            },
        },
        new Restaurant(){

            Name = "McDonald",
            Category = "Fast Food",
            Description = "McDonalfd's Corporation (McDonald's)",
            ContactEmail = "contact@mcdonald.com",
            HasDelivery = true,
            Address = new Address()
            {
                City = "London",
                Street = "Boots 193",
                PostalCode = "W1F 8SR",
            },
        },
    ];

        return restaurants;
    }


}
