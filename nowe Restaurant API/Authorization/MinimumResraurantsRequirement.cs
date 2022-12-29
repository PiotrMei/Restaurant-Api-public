using Microsoft.AspNetCore.Authorization;

namespace nowe_Restaurant_API.Authorization
{
    public class MinimumResraurantsRequirement:IAuthorizationRequirement
    {
        public int _minrestaurants { get; }
        public MinimumResraurantsRequirement(int minrestaurants)
        {
            _minrestaurants = minrestaurants; 
        }
        
    }
}
