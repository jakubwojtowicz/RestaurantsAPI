using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public bool ContactEmail { get; set; }
        public bool ContactPhone { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string Region { get; set; }
    }
}
