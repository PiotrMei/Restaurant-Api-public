using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using nowe_Restaurant_API.Authorization;
using nowe_Restaurant_API.Entities;
using nowe_Restaurant_API.Exceptions;
using nowe_Restaurant_API.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace nowe_Restaurant_API.services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly ILogger<RestaurantServices> _logger;
        private readonly IAuthorizationService authorizationServices;
        private readonly IUserContextService userContextService;

        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger, IAuthorizationService authorizationServices, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            this.authorizationServices = authorizationServices;
            this.userContextService = userContextService;
        }

        public PageResult<RestaurantDto> GetRestaurants(SearchQuery searchQuery)
        {
            var Baserestaurants = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                 .Include(r => r.Dishes)
                 .Where(s => searchQuery.searchby == null || s.Name.ToLower().Contains(searchQuery.searchby.ToLower())
                 || s.Destripcion.ToLower().Contains(searchQuery.searchby.ToLower()));
            
            if (!string.IsNullOrEmpty(searchQuery.sortby))
            {
                var columnsselector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r=> r.Name },
                    {nameof(Restaurant.Destripcion), r=> r.Destripcion },
                    {nameof(Restaurant.Category), r=> r.Category },
                };
                var selectedcolumn = columnsselector[searchQuery.sortby];
                Baserestaurants = searchQuery.sortdirection == SortDirection.Asc ? Baserestaurants.OrderBy(selectedcolumn) :
                     Baserestaurants.OrderByDescending(selectedcolumn);
            }
            
            var totalresults = Baserestaurants.Count();
               var restaurants = Baserestaurants  
                 .Skip(searchQuery.pagesize*(searchQuery.pagenumber-1))
                 .Take(searchQuery.pagesize)
                 .ToList();
            var restaurantsdto = _mapper
                .Map<List<RestaurantDto>>(restaurants);

            var result = new PageResult<RestaurantDto>(restaurantsdto, searchQuery.pagenumber, searchQuery.pagesize, totalresults);

            //var authorizationresult = authorizationServices.AuthorizeAsync(userContextService.User, restaurants, new MinimumResraurantsRequirement(2)).Result;
            //if (!authorizationresult.Succeeded)
            //{
            //    throw new ForbiddenException();
            //}
            return result;
        }

        public RestaurantDto GetRestaurantByID(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant Not Found");
            }
        
            var restaurantdto = _mapper
                .Map<RestaurantDto>(restaurant);
            return restaurantdto;
        }

        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById=userContextService.getUserId;
            _dbContext.Add(restaurant);
            _dbContext.SaveChanges();
            var id = restaurant.Id;
            return id;
        }

        public void RemoveRestaurant (int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETED");
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id ==id);  
            
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant Not Found");
            }
            var authorizationresult = authorizationServices.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequiment(ResourceOperation.Delete)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbiddenException();
            }

            _dbContext.Remove(restaurant);
            _dbContext.SaveChanges();
            
        }

        public void PutRestaurant(int id, PutRestaurantDto putRestaurantDto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                throw new NotFoundException("Restaurant Not Found");
            }
            var authorizationresult = authorizationServices.AuthorizeAsync(userContextService.User, restaurant, new ResourceOperationRequiment(ResourceOperation.Update)).Result;
            if (!authorizationresult.Succeeded)
            {
                throw new ForbiddenException();
            }
            //var putrestaurant = _mapper.Map<Restaurant>(putRestaurantDto);
            restaurant.Name = putRestaurantDto.Name;
            restaurant.Destripcion = putRestaurantDto.Destripcion;
            restaurant.HasDelivery = putRestaurantDto.HasDelivery;
            //_dbContext.Update(restaurant);
            //_dbContext.Update(restaurant.Name = putrestaurant.Name, restaurant.Destripcion = putrestaurant.Destripcion, restaurant.HasDelivery = putrestaurant.HasDelivery);
            _dbContext.SaveChanges();
            
        }
    }
}