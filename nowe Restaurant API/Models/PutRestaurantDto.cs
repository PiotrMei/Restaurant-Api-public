using System.ComponentModel.DataAnnotations;

namespace nowe_Restaurant_API.Models
{
    public class PutRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Destripcion { get; set; }
        public bool HasDelivery { get; set; }
    }
}
