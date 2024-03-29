﻿namespace nowe_Restaurant_API.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Destripcion { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public virtual List<DishDto> Dishes { get; set; }

    }
}
