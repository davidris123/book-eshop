using Eshop.DomainEntities;
using Eshop.Repository.Interface;
using EShop.Repository.Interface;
using Eshop.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Eshop.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _productRepository;
        private readonly IRepository<BooksInShoppingCart> _booksInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IRepository<Book> productRepository, IRepository<BooksInShoppingCart> booksInShoppingCartRepository, IUserRepository userRepository, ILogger<BookService> logger)
        {
            _productRepository = productRepository;
            _booksInShoppingCartRepository = booksInShoppingCartRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public void CreateNewBook(Book p)
        {
            _productRepository.Insert(p);
            
        }

        public void DeleteBook(Guid id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }

        public List<Book> GetAllBooks()
        {
            return _productRepository.GetAll().ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public void UpdeteExistingBook(Book p)
        {
            _productRepository.Update(p);
        }
    }
}
