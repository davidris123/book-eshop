using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.DomainEntities.DTO
{
    public class BookDto
    {
        public string? name {  get; set; }
        public string? description { get; set; }
        public string? imageurl { get; set; }
        public int price { get; set; }

    }
}
