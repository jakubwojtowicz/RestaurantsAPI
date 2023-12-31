﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);

        PageResult<RestaurantDto> GetAll(RestaurantQuery query);

        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService:IRestaurantService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<RestaurantService> logger;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public RestaurantService(RestaurantDbContext context, IMapper mapper, ILogger<RestaurantService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        public void Delete(int id)
        {
            logger.LogWarning($"Restaurant with id: {id} DELETE action invoked");
            var restaurant = context
                .Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");


            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

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

            var authorizationResult = authorizationService.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

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

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = context.Restaurants
                                .Include(r => r.Address)
                                .Include(r => r.Dishes)
                                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                                    || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r=>r.Name},
                    { nameof(Restaurant.Description), r=>r.Description},
                    { nameof(Restaurant.Category), r=>r.Category}
                };
                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Asc 
                    ? baseQuery.OrderBy(selectedColumn) 
                    : baseQuery.OrderByDescending(selectedColumn);
            }
            var restaurants = baseQuery
                .Skip(query.PageSize*(query.PageNumber-1))
                .Take(query.PageSize)
                .ToList();

            var restaurantsDto = mapper.Map<List<RestaurantDto>>(restaurants);

            var result = new PageResult<RestaurantDto>(restaurantsDto, baseQuery.Count(), query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            restaurant.CreatedByID = userContextService.GetUserId;
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            return restaurant.Id;
        }
    }
}
