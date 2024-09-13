using Eshop.DomainEntities;
using Eshop.DomainEntities.DTO;
using Eshop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebApplication1.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        public AdminController(IOrderService orderService, IBookService bookService)
        {
            _orderService = orderService;
            _bookService = bookService;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }
        [HttpGet("[action]")]
        public List<Book> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }
        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return _orderService.GetDetailsForOrder(model);
        }

        [HttpPost("[action]")]
        public bool ImportAllBooks(List<BookDto> model)
        {
            bool status = true;

            foreach (var item in model)
            {
               
                    var book = new Book
                    {
                        Name = item.name,
                        Description = item.description,
                        ImageUrl = item.imageurl,
                        Price = item.price
                    };

                    _bookService.CreateNewBook(book);
            }
            return status;
        }

    }
}
