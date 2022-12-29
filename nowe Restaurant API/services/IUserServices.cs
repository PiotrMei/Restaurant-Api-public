using nowe_Restaurant_API.Models;

namespace nowe_Restaurant_API.services
{
    public interface IUserServices
    {
        void CreateUser(UserDto userDto);
        string LoginUser(LoginUserDto loginUserDto);
    }
}