using Microsoft.AspNetCore.Identity;
using Restaurants.Application.Common.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                await dbContext.Users.AddRangeAsync(users);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                await dbContext.Roles.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.UserRoles.Any())
            {
                var userRoles = GetUserRoles();
                await dbContext.UserRoles.AddRangeAsync(userRoles);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
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
                Description = "KFC is an American fast food restaurant chain specializing in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Spicy fried chicken with a crispy coating (10 pcs.).",
                        Price = 10.30M
                    },
                    new()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Crispy chicken nuggets (5 pcs.).",
                        Price = 5.30M
                    },
                    new()
                    {
                        Name = "Zinger Burger",
                        Description = "Spicy crispy chicken fillet burger.",
                        Price = 7.50M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DI"
                }
            },

            new()
            {
                Name = "Pizza Hut",
                Category = "Italian",
                Description = "Pizza Hut is an international restaurant chain known for pizza, pasta, and Italian-inspired dishes.",
                ContactEmail = "contact@pizzahut.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Margherita Pizza",
                        Description = "Classic pizza with tomato sauce, mozzarella, and fresh basil.",
                        Price = 12.50M
                    },
                    new()
                    {
                        Name = "Pepperoni Pizza",
                        Description = "Pizza topped with mozzarella cheese and pepperoni.",
                        Price = 15.90M
                    },
                    new()
                    {
                        Name = "Chicken Alfredo Pasta",
                        Description = "Creamy Alfredo pasta with grilled chicken.",
                        Price = 13.40M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Oxford Street 120",
                    PostalCode = "W1D 1LT"
                }
            },

            new()
            {
                Name = "Sushi House",
                Category = "Japanese",
                Description = "A Japanese restaurant serving freshly prepared sushi, sashimi, and traditional Japanese dishes.",
                ContactEmail = "info@sushihouse.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Salmon Sushi",
                        Description = "Fresh salmon nigiri served with seasoned rice.",
                        Price = 9.80M
                    },
                    new()
                    {
                        Name = "California Roll",
                        Description = "Sushi roll with avocado, cucumber, and crab-style filling.",
                        Price = 8.50M
                    },
                    new()
                    {
                        Name = "Chicken Teriyaki",
                        Description = "Grilled chicken glazed with teriyaki sauce and served with rice.",
                        Price = 14.20M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Baker Street 221",
                    PostalCode = "NW1 6XE"
                }
            },

            new()
            {
                Name = "Taco Bell",
                Category = "Mexican",
                Description = "A fast food restaurant offering Mexican-inspired tacos, burritos, and quesadillas.",
                ContactEmail = "support@tacobell.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Crunchy Taco",
                        Description = "Crispy taco shell filled with seasoned beef, lettuce, and cheese.",
                        Price = 3.50M
                    },
                    new()
                    {
                        Name = "Chicken Burrito",
                        Description = "Flour tortilla filled with chicken, rice, beans, and salsa.",
                        Price = 8.90M
                    },
                    new()
                    {
                        Name = "Cheese Quesadilla",
                        Description = "Grilled tortilla filled with melted cheese.",
                        Price = 6.70M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Kingsway 45",
                    PostalCode = "WC2B 6SR"
                }
            },

            new()
            {
                Name = "Burger King",
                Category = "American",
                Description = "An American fast food chain famous for flame-grilled burgers, fries, and sandwiches.",
                ContactEmail = "contact@burgerking.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Whopper",
                        Description = "Flame-grilled beef burger with lettuce, tomato, onion, and mayonnaise.",
                        Price = 9.50M
                    },
                    new()
                    {
                        Name = "Double Cheeseburger",
                        Description = "Two beef patties with melted cheese in a toasted bun.",
                        Price = 8.70M
                    },
                    new()
                    {
                        Name = "Chicken Royale",
                        Description = "Crispy chicken fillet burger with lettuce and mayonnaise.",
                        Price = 7.90M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Strand 88",
                    PostalCode = "WC2R 0DW"
                }
            },

            new()
            {
                Name = "Curry Palace",
                Category = "Indian",
                Description = "An Indian restaurant serving traditional curries, aromatic rice dishes, and freshly baked naan bread.",
                ContactEmail = "info@currypalace.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Chicken Tikka Masala",
                        Description = "Grilled chicken in a creamy tomato-based curry sauce.",
                        Price = 14.50M
                    },
                    new()
                    {
                        Name = "Butter Chicken",
                        Description = "Tender chicken cooked in a rich buttery tomato sauce.",
                        Price = 13.90M
                    },
                    new()
                    {
                        Name = "Vegetable Biryani",
                        Description = "Fragrant basmati rice cooked with vegetables and Indian spices.",
                        Price = 11.80M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Brick Lane 25",
                    PostalCode = "E1 6PU"
                }
            },

            new()
            {
                Name = "Green Garden",
                Category = "Vegetarian",
                Description = "A vegetarian restaurant focused on fresh ingredients, healthy meals, and plant-based cuisine.",
                ContactEmail = "hello@greengarden.com",
                HasDelivery = false,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Vegetable Burger",
                        Description = "Plant-based burger with lettuce, tomato, and house sauce.",
                        Price = 9.20M
                    },
                    new()
                    {
                        Name = "Grilled Vegetable Bowl",
                        Description = "A bowl of seasonal grilled vegetables served with quinoa.",
                        Price = 10.50M
                    },
                    new()
                    {
                        Name = "Mushroom Risotto",
                        Description = "Creamy risotto prepared with mushrooms and Parmesan cheese.",
                        Price = 12.60M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Marylebone High Street 32",
                    PostalCode = "W1U 4QH"
                }
            },

            new()
            {
                Name = "Dragon Wok",
                Category = "Chinese",
                Description = "A Chinese restaurant offering stir-fried noodles, rice dishes, and traditional Asian specialties.",
                ContactEmail = "contact@dragonwok.com",
                HasDelivery = true,
                OwnerId = "0af2138d-b32e-4f83-b6cd-3c162a3f6a0e",
                Dishes =
                [
                    new()
                    {
                        Name = "Chicken Chow Mein",
                        Description = "Stir-fried noodles with chicken and fresh vegetables.",
                        Price = 10.90M
                    },
                    new()
                    {
                        Name = "Sweet and Sour Chicken",
                        Description = "Crispy chicken served with sweet and sour sauce.",
                        Price = 12.40M
                    },
                    new()
                    {
                        Name = "Vegetable Fried Rice",
                        Description = "Fried rice with eggs, vegetables, and soy sauce.",
                        Price = 8.60M
                    }
                ],
                Address = new()
                {
                    City = "London",
                    Street = "Chinatown, Gerrard Street 18",
                    PostalCode = "W1D 6JD"
                }
            }
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
}
