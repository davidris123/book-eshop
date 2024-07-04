using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainEntities
{
    public class Book : BaseEntity
    {
        public string? Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? Author { get; set; }

        public string? Publisher { get; set; }

        public string? Genre { get; set; }

        public DateTime? Published { get; set; }

        public int Stock { get; set; }
    }
}
