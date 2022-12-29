using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nowe_Restaurant_API.Entities;
using nowe_Restaurant_API.Exceptions;
using nowe_Restaurant_API.Models;

namespace nowe_Restaurant_API.services
{
    public class DishServices : IDishServices
    {
        private readonly RestaurantDbContext dbContext;
        private readonly IMapper mapper;

        public DishServices(RestaurantDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public int CreateDish(int restaurantID, CreateDishDto dishDto)
        {
            var restaurant = GetRestaurantByIDhelper(restaurantID);

            var newdish = mapper
                 .Map<Dish>(dishDto);

            newdish.RestaurantId = restaurantID;

            dbContext.Add(newdish);
            dbContext.SaveChanges();

            return newdish.Id;
        }

        public DishDto GetDishById (int RestaurantId, int DishId)
        {
            var restaurant = GetRestaurantByIDhelper(RestaurantId);
            // var Dish = restaurant.Dishes.FirstOrDefault(d => d.Id == DishId);
            var Dish = dbContext.Dishes.FirstOrDefault(d => d.Id == DishId);
            if (Dish == null || Dish.RestaurantId != RestaurantId) throw new NotFoundException("Dish not foun");

            var DishDto = mapper.Map<DishDto>(Dish);

            return DishDto;
        }
        public IEnumerable<DishDto> GetDish (int restaurantID)
        {
            var restaurant = GetRestaurantByIDhelper(restaurantID);

            //var Dishes = restaurant.Dishes;
            var DishesDto = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return DishesDto;

        }

        public void DeleteAllDishes (int restaurantID)
        {
            var restaurant = GetRestaurantByIDhelper(restaurantID);

            if (restaurant.Dishes == null) throw new NotFoundException("Dish not found");
            dbContext.RemoveRange(restaurant.Dishes);
            dbContext.SaveChanges();

        }

        public void DeleteDishById(int restaurantID, int dishId)
        {
            var restaurant = GetRestaurantByIDhelper(restaurantID);
            var dish = dbContext
                .Dishes
                .FirstOrDefault(r => r.Id == dishId);
            if (dish == null) throw new NotFoundException("Dish not found");

            
            dbContext.Remove(dish);
            dbContext.SaveChanges();

        }

        private Restaurant GetRestaurantByIDhelper(int restaurantID)
        {
            var restaurant = dbContext
                            .Restaurants
                            .Include(r => r.Dishes)
                            .FirstOrDefault(d => d.Id == restaurantID);
            if (restaurant == null) throw new NotFoundException("Restaurant not found");
            return restaurant;
        }
    }
}
