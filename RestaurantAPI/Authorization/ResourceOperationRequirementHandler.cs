using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Restaurant restaurant)
        {
            //every user can read and create content
            if(requirement.Operation == ResourceOperation.Read ||
                requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            //get user id
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            //check if this user created this restaurant
            if (restaurant.CreatedByID == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
