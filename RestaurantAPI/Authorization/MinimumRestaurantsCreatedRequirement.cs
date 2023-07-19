using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreatedRequirement: IAuthorizationRequirement
    {
        public int numberOfRestaurantsCreated { get; }

        public MinimumRestaurantsCreatedRequirement(int numberOfRestaurantsCreated)
        {
            this.numberOfRestaurantsCreated = numberOfRestaurantsCreated;
        }

    }
}
