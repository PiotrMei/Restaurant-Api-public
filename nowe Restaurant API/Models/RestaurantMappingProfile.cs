using AutoMapper;
using nowe_Restaurant_API.Entities;

namespace nowe_Restaurant_API.Models
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(d => d.Adress.City))
                .ForMember(m => m.Street, c => c.MapFrom(d => d.Adress.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(d => d.Adress.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(m => m.Adress, c => c.MapFrom(dto => new Adress 
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));
           
            CreateMap<PutRestaurantDto, Restaurant>();

            CreateMap<CreateDishDto, Dish>();

            CreateMap<User, UserDto>()
            .ForMember(u => u.RoleId, a => a.MapFrom(d => d.Role.Id));

        }
    }
}
