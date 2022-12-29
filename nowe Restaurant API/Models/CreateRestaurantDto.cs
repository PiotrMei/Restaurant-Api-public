using System.ComponentModel.DataAnnotations;

namespace nowe_Restaurant_API.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Destripcion { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactNumer { get; set; }
        public string ContactEmail { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        [Required]
        [MaxLength(25)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
