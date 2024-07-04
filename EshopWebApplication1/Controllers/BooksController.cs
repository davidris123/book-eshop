using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eshop.DomainEntities;
using System.Security.Claims;
using Eshop.Service.Interface;

namespace EshopWebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IShoppingCartService _shoppingCartService;

        public BooksController(IBookService bookService, IShoppingCartService shoppingCartService)
        {
            _bookService = bookService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_bookService.GetAllBooks());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            var product = _bookService.GetDetailsForBook(id); 
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            { 
                return View(book);
            }
            _bookService.CreateNewBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(BooksInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            if (userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var book = _bookService.GetDetailsForBook(model.BookId);
            if (model.Quantity > book.Stock)
            {
                return Redirect("/Books/IncorrectAmount");
            }
            else
            {
                //book.Stock -= model.Quantity;
                //_bookService.UpdeteExistingBook(book);
                _shoppingCartService.AddToShoppingConfirmed(model, userId);
                return View("Index", _bookService.GetAllBooks());
            }
        }

        public IActionResult IncorrectAmount()
        {
            return View();
        }
        public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _bookService.GetDetailsForBook(id);
            BooksInShoppingCart ps = new()
            {
                BookId = book.Id
            };
            return View(ps);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _bookService.GetDetailsForBook(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Price,Description,ImageUrl,Author,Publisher,Genre,Published,Stock")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdeteExistingBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var product = _bookService.GetDetailsForBook(id);
            if (product != null)
            {
                _bookService.DeleteBook(id);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
