using Eshop.DomainEntities;

namespace Eshop.DomainEntities
{
    public class ShoppingCartDto
    {
        public List<BooksInShoppingCart>? BooksInShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
