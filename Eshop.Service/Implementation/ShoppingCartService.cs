using Eshop.DomainEntities;
using Eshop.Repository.Interface;
using EShop.Repository.Interface;
using Eshop.Service.Interface;
using System.Text;

namespace Eshop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<BooksInOrder> _booksInOrderRepository;
        private readonly IRepository<BooksInShoppingCart> _booksInShoppingCartRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<EmailMessage> mailRepository, IRepository<BooksInOrder> booksInOrderRepository, IRepository<BooksInShoppingCart> booksInShoppingCartRepository, IRepository<Book> bookRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _mailRepository = mailRepository;
            _booksInOrderRepository = booksInOrderRepository;
            _booksInShoppingCartRepository = booksInShoppingCartRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public bool deleteBookFromShoppingCart(string userId, Guid bookId)
        {
            if (!string.IsNullOrEmpty(userId) && bookId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.BooksInShoppingCarts.Where(z => z.BookId.Equals(bookId)).FirstOrDefault();

                userShoppingCart.BooksInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }
        public ShoppingCartDto getShoppingCartInfo(string id)
        {

            var user = this._userRepository.Get(id);
            var shoppingCart = user.UserCart;

            ShoppingCartDto model = new ShoppingCartDto()
            {
                BooksInShoppingCarts = shoppingCart.BooksInShoppingCarts ?? [],
                TotalPrice = shoppingCart.BooksInShoppingCarts.Sum(z => z.Quantity * z.Book.Price)

            };
            return model;
        }

        public bool AddToShoppingConfirmed(BooksInShoppingCart model, string userId)
        {
            var user = this._userRepository.Get(userId);
            var shoppingCart = user.UserCart;
            BooksInShoppingCart itemToAdd = new BooksInShoppingCart
            {
                Id = Guid.NewGuid(),
                Book = model.Book,
                BookId = model.BookId,
                ShoppingCart = shoppingCart,
                ShoppingCartId = shoppingCart.Id,
                Quantity = model.Quantity
            };

            _booksInShoppingCartRepository.Insert(itemToAdd);
            return true;

        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                EmailMessage message = new()
                {
                    Subject = "Successful order",
                    MailTo = loggedInUser.Email
                };

                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    EshopApplicationUserId = userId
                };
                this._orderRepository.Insert(order);

                List<BooksInOrder> booksInOrders = [];

                var result = userCard.BooksInShoppingCarts.Select(z => new BooksInOrder
                {
                    Id = Guid.NewGuid(),
                    BookId = z.Book.Id,
                    Book = z.Book,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();

                foreach (var book in result)
                {
                    var each_book = _bookRepository.Get(book.BookId);
                    each_book.Stock -= book.Quantity;
                    _bookRepository.Update(each_book);
                }

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order contains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Book.Name + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Book.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();


                booksInOrders.AddRange(result);

                foreach (var item in booksInOrders)
                {
                    this._booksInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.BooksInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(message);
                this._emailService.SendEmailAsync(message);
                return true;
            }

            return false;
        }
    }
}
