using nowe_Restaurant_API.Models;
using System.Security.Claims;

namespace nowe_Restaurant_API.services
{
    public interface IRestaurantServices
    {
        int CreateRestaurant(CreateRestaurantDto dto);
        RestaurantDto GetRestaurantByID(int id);
        PageResult<RestaurantDto> GetRestaurants(SearchQuery searchQuery);
        void RemoveRestaurant(int id);
        void PutRestaurant(int id, PutRestaurantDto putRestaurantDto);
        
    }
}