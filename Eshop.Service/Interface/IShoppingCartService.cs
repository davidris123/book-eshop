using Eshop.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteBookFromShoppingCart(string userId, Guid bookId);
        bool order(string userId);
        bool AddToShoppingConfirmed(BooksInShoppingCart model, string userId);
    }
}
