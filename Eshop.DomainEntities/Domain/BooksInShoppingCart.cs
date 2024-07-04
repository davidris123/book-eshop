using System.ComponentModel.DataAnnotations;

namespace Eshop.DomainEntities
{
    public class BooksInShoppingCart : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
