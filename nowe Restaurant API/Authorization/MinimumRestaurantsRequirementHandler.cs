using Microsoft.AspNetCore.Authorization;
using nowe_Restaurant_API.Entities;
using System.Security.Claims;

namespace nowe_Restaurant_API.Authorization
{
    public class MinimumRestaurantsRequirementHandler : AuthorizationHandler<MinimumResraurantsRequirement, List<Restaurant>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumResraurantsRequirement requirement, List<Restaurant> resource)
        {
            var userid = int.Parse(context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var restaurants = resource.FindAll(u => u.CreatedById == userid).Count;
         if (restaurants >= requirement._minrestaurants)
            {
                context.Succeed(requirement);
                 
            }
            return Task.CompletedTask;

          
        }
    }
}
