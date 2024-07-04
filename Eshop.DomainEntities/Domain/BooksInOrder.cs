using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainEntities
{
    public class BooksInOrder : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid OrderId { get; set; }
        public Order? UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
