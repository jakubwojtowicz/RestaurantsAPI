using RestaurantAPI.Entities;
using System.Collections.Generic;

namespace RestaurantAPI.Models
{
    public class RestaurantDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}
