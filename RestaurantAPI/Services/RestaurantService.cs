using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);

        IEnumerable<RestaurantDto> GetAll();

        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService:IRestaurantService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantService> logger;

        public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public void Delete(int id)
        {
            logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            context.Restaurants.Remove(restaurant);
            context.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if (restaurant is null) 
                throw new NotFoundException("Restaurant not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            context.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = context
                    .Restaurants
                    .Include(r => r.Address)
                    .Include(r => r.Dishes)
                    .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = context.Restaurants
                .Include(r => r.Address)
                .Include(r=> r.Dishes)
                .ToList();

            var restaurantsDto = mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDto;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            return restaurant.Id;
        }
    }
}
