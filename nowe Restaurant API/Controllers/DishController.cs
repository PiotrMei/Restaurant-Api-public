using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nowe_Restaurant_API.Models;
using nowe_Restaurant_API.services;

namespace nowe_Restaurant_API.Controllers
{
    [Route("api/Restaurant/{restaurantID}/Dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishServices dishServices;//aa

        public DishController(IDishServices dishServices)
        {
            this.dishServices = dishServices;
        }

        [HttpPost]
        public ActionResult CreateDish([FromRoute] int restaurantID, [FromBody] CreateDishDto dto)
        {
            var DishId = dishServices.CreateDish(restaurantID, dto);

            return Created($"api/restaurant/{restaurantID}/dish/{DishId}", null);
        }

        [HttpGet("{DishId}")]
        public ActionResult<DishDto> GetDishByID([FromRoute] int restaurantID, [FromRoute] int DishId)
        {
            var Dish = dishServices.GetDishById(restaurantID, DishId);
            return Ok(Dish);
        }
        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetDish([FromRoute] int restaurantID)
        {
            var Dishes = dishServices.GetDish(restaurantID);
            return Ok(Dishes);
        }

        [HttpDelete]
        public ActionResult DeleteAllDishes([FromRoute] int restaurantID)
        {
            dishServices.DeleteAllDishes(restaurantID);
            return NoContent();

        }
        [HttpDelete("{dishId}")]
        public ActionResult DeleteDishById([FromRoute] int restaurantID, [FromRoute] int dishId)
        {
            dishServices.DeleteDishById(restaurantID, dishId);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<DishDto> GetDishbyname()
        {
            DishDto Dish = new DishDto();
            return Dish;
            //aaa
            //bbbb
        }
    }
}
