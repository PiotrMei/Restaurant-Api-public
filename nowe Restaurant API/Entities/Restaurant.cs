﻿namespace nowe_Restaurant_API.Entities
{
    public class Restaurant
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        public string Destripcion { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public string ContactNumer { get; set; }
        public string ContactEmail { get; set; }
        public int AdressID { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual List<Dish> Dishes { get; set; }

    }
}
