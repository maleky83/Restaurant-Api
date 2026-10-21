using Microsoft.AspNetCore.Identity;
using Restaurants.Application.Constants;
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

        if (!dbContext.Roles.Any())
        {
            var roles = GetRoles();
            await dbContext.Roles.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Users.Any())
        {
            var users = GetUsers();
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.UserRoles.Any())
        {
            var userRoles = GetUserRoles();
            await dbContext.UserRoles.AddRangeAsync(userRoles);
            await dbContext.SaveChangesAsync();
        }
    }

    private IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = [
            new()
            {
                Id="0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Email="owner@gmail.com",
                UserName="owner@gmail.com",
                NormalizedEmail="owner@gmail.com".ToUpper(),
                NormalizedUserName="owner@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2000,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A==",
            },
            new()
            {
                Id="413da784-ecab-4248-b786-d5c6214b9786",
                Email="user@gmail.com",
                UserName="user@gmail.com",
                NormalizedEmail="user@gmail.com".ToUpper(),
                NormalizedUserName="user@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2005,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A==",
            },
            new()
            {
                Id="f875a48f-21b6-4ff2-8d42-bddabb8244f5",
                Email="admin@gmail.com",
                UserName="admin@gmail.com",
                NormalizedEmail="admin@gmail.com".ToUpper(),
                NormalizedUserName="admin@gmail.com".ToUpper(),
                Nationality="iran",
                DateOfBirth=new DateOnly(2010,1,10),
                PasswordHash="AQAAAAIAAYagAAAAEPicYtOaWyOvVqKDzy46TUqdAZbRCKTdsDZl6qdCt4HYJRB+m3vLiiy9QNJlaggm+A=="
            }
            ];
        return users;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        IEnumerable<IdentityRole> roles = [
            new(UserRoles.Admin)
            {
                Id="8f5bbaf0-d4cb-4467-b674-6776d22cca93",
                NormalizedName=UserRoles.Admin.ToUpper()
            },
            new(UserRoles.User)
            {
                Id="6a47498d-96de-4b15-a5d0-acf1833e765b",
                NormalizedName=UserRoles.User.ToUpper()
            },
            new(UserRoles.Owner)
            {
                Id="987cb21b-9174-48be-bc7d-c2778bf24c82",
                NormalizedName=UserRoles.Owner.ToUpper()
            }];
        return roles;
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

    private IEnumerable<IdentityUserRole<string>> GetUserRoles()
    {
        IEnumerable<IdentityUserRole<string>> userRoles = [
            new()
            {
                UserId="f875a48f-21b6-4ff2-8d42-bddabb8244f5",
                RoleId="8f5bbaf0-d4cb-4467-b674-6776d22cca93"

            },
            new()
            {
                RoleId="6a47498d-96de-4b15-a5d0-acf1833e765b",
                UserId="413da784-ecab-4248-b786-d5c6214b9786"
            },
            new()
            {
                RoleId="987cb21b-9174-48be-bc7d-c2778bf24c82",
                UserId="0af2138d-b32e-4f83-b6cd-3c162a3f6a0e"
            }];

        return userRoles;
    }
}
