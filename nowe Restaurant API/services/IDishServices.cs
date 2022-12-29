using nowe_Restaurant_API.Models;

namespace nowe_Restaurant_API.services
{
    public interface IDishServices
    {
        int CreateDish(int Id, CreateDishDto dishDto);
        DishDto GetDishById(int RestaurantId, int DishId);
        IEnumerable<DishDto> GetDish(int restaurantID);
        void DeleteAllDishes(int restaurantID);
        void DeleteDishById(int restaurantID, int dishId);
    }
}