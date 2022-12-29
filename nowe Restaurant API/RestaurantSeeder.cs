using Microsoft.EntityFrameworkCore;
using nowe_Restaurant_API.Entities;

namespace nowe_Restaurant_API
{

    public class RestaurantSeeder
    {

        private readonly RestaurantDbContext _dbcontext;

        public RestaurantSeeder(RestaurantDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void seed()
        {
            if (_dbcontext.Database.CanConnect())
            {
                var pendingmigration = _dbcontext.Database.GetPendingMigrations();  
                if (pendingmigration != null && pendingmigration.Any())
                {
                    _dbcontext.Database.Migrate();
                }

                if (!_dbcontext.Roles.Any())
                {
                    var Roles = GetRoles();
                    _dbcontext.Roles.AddRange(Roles);
                    _dbcontext.SaveChanges();
                }

                if (!_dbcontext.Restaurants.Any())
                {
                    var restaurants = GetRestaurant();
                    _dbcontext.Restaurants.AddRange(restaurants);
                    _dbcontext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="User",
                },
         new Role()
        {
            Name = "Manager",
        },
         new Role()
        {
            Name = "Admin",
        },
            };
            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurant()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                 Name = "Nalesnikarnia",
                 Destripcion = "Dobre jedzenie",
                 Category ="Restauracja",
                 HasDelivery = false,
                 ContactNumer = "6665555222",
                 ContactEmail = "aaa@bbb.com",
                 Adress = new Adress ()
                 {
                       City = "Mikolow",
                         Street = "Rynek",
                        PostalCode ="43-190"
                 },
                 Dishes = new List<Dish>()
                 {
                     new Dish()
                     {
                         Name = "Nalesnik z miesem",
                         Destription = "mieso",
                         Price = 12
                     },
                      new Dish()
                     {
                         Name = "Nalesnik z serem",
                         Destription = "ser",
                         Price = 10
                     },
                       new Dish()
                     {
                         Name = "placek",
                         Destription = "ziemniak",
                         Price = 8
                     },
                 },
                },
                 new Restaurant()
                {
                 Name = "McDonalds",
                 Destripcion = "Szybkie jedzenie",
                 Category ="Fast Food",
                 HasDelivery = true,
                 ContactNumer = "777222555",
                 ContactEmail = "bbb@ccc.com",
                 Adress = new Adress ()
                 {
                       City = "Mikolow",
                         Street = "Wislanka",
                        PostalCode ="43-190"
                 },
                 Dishes = new List<Dish>()
                 {
                     new Dish()
                     {
                         Name = "Burger",
                         Destription = "mieso",
                         Price = 6
                     },
                      new Dish()
                     {
                         Name = "CheseBurger",
                         Destription = "mieso+ser",
                         Price = 10
                     },
                       new Dish()
                     {
                         Name = "Frytki",
                         Destription = "ziemniak",
                         Price = 4
                     }
                 }
                 }
            };
                 
          return restaurants;
        }

    }
}

