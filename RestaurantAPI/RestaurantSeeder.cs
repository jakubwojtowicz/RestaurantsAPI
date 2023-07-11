using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _context;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    _context.SaveChanges();
                }
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Name = "User"
                },
                new Role
                {
                    Name = "Menager"
                },
                new Role
                {
                    Name = "Administrator"
                }
            };

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant
                {
                    Name = "McDonalds",
                    Category = "FastFood",
                    Description = "McDonald's Corporation – amerykańska sieć barów szybkiej obsługi, sprzedająca głównie burgery, frytki i napoje.",
                    ContactEmail = true,
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Big Mac",
                            Price = 10.3M
                        },
                        new Dish()
                        {
                            Name = "McDouble",
                            Price = 3.5M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Lublin",
                        Country = "Poland",
                        PostalCode = "12345",
                        Region = "Poland"
                    }

                }
            };

            return restaurants;
        }
    }
}
