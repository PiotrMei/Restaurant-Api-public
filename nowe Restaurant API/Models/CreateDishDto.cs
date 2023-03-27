using System.ComponentModel.DataAnnotations;

namespace nowe_Restaurant_API.Models
{
    public class CreateDishDto
    {
        [Required]
        public string Name { get; set; }
        public string Destription { get; set; }
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        
    }
}
