using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreatedRequirementHandler : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IUserContextService userContextService;

        public MinimumRestaurantsCreatedRequirementHandler(RestaurantDbContext dbContext, IUserContextService userContextService)
        {
            this.dbContext = dbContext;
            this.userContextService = userContextService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreatedRequirement requirement)
        {
            int numberOfUsersRestaurants = CalculateNumberOfUsersRestaurants((int)userContextService.GetUserId);
            if(numberOfUsersRestaurants < requirement.numberOfRestaurantsCreated)
            {
                throw new ForbidException();
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private int CalculateNumberOfUsersRestaurants(int userId)
        {
            int numberOfUsersRestaurants = 0;
            var restaurants = dbContext.Restaurants.ToList();
            foreach(var restaurant in restaurants)
            {
                if(restaurant.CreatedByID == userId)
                {
                    numberOfUsersRestaurants++;
                }
            }
            return numberOfUsersRestaurants;
        }
    }
}
