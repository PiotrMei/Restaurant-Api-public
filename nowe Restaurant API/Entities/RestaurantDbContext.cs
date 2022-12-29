using Microsoft.EntityFrameworkCore;

namespace nowe_Restaurant_API.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
        //private string _configurationstring = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb2;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired(false);

            modelBuilder.Entity<User>()
    .Property(u => u.PasswordHash)
    .IsRequired(false);

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Adress>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Adress>()
               .Property(a => a.Street)
               .IsRequired()
               .HasMaxLength(50);


            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();
            //modelBuilder.Entity<Dish>()
            //    .Property(d => d.Price)
            //    .HasColumnType("var")
            //    .HasPrecision(2)
            //    .HasConversion<decimal>();
            
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_configurationstring);
        //}
    }
}
