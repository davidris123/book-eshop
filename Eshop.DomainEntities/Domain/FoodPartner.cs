using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainEntities.Domain
{
    public class FoodPartner
    {
        public Guid Id { get; set; }
        public string? FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public string? FoodImage { get; set; }
        public int Price { get; set; } 
        public Guid RestaurantId { get; set; }
    }
}
