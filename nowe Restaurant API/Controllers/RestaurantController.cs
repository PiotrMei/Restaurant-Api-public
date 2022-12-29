using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nowe_Restaurant_API.Entities;
using nowe_Restaurant_API.Models;
using nowe_Restaurant_API.services;
using System.Security.Claims;

namespace nowe_Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        [HttpGet]
       // [Authorize(Policy = "Has20")]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurant([FromQuery] SearchQuery searchQuery)
        {
            
            HttpContext.User.IsInRole("Admin");
            var restaurantsDto = _restaurantServices.GetRestaurants(searchQuery);
            //Thread.Sleep(4001);
            return Ok(restaurantsDto);
                
            
            //return BadRequest("Blad");    
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "HasNationality")]
        public ActionResult AddRestaurant([FromBody] CreateRestaurantDto Dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);  
            //}
            var userID = int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            var restaurant =_restaurantServices.CreateRestaurant(Dto);
            return Created($"/api/Restaurant/{restaurant}", null);
        }


        [HttpGet("{ID}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> GetRestaurantByID([FromRoute] int ID)
        {
            
                
                   var restaurant = _restaurantServices.GetRestaurantByID(ID);  

            //            if (restaurant == null)
            //{
            //   return NotFound();
            //}
            return Ok(restaurant);   
   
        }

        [HttpDelete("{ID}")]
        public ActionResult RemoveRestaurant([FromRoute] int ID)
        {
            _restaurantServices.RemoveRestaurant(ID);
            
                return NoContent();
            
        }

        [HttpPut("{ID}")]
        public ActionResult PutRestaurant([FromRoute] int ID, [FromBody] PutRestaurantDto putRestaurantDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            _restaurantServices.PutRestaurant(ID, putRestaurantDto);
           
             return Ok();
          

        }

    }

    

}
