using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nowe_Restaurant_API.Models;
using nowe_Restaurant_API.services;

namespace nowe_Restaurant_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] UserDto userDto)
        {
            userServices.CreateUser(userDto);
            return Ok(); 
        }

        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var token = userServices.LoginUser(loginUserDto);
            return Ok(token);
        }
    }
}
