using Microsoft.AspNetCore.Mvc;
using EshopWebApplication1.Data;
using Eshop.DomainEntities;
using System.Security.Claims;
using Eshop.Service.Interface;
using Stripe;
using Microsoft.Extensions.Options;

namespace EshopWebApplication1.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService? shoppingCartService,IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
        }
        public IActionResult Index()
        {
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult UnsuccessfulPayment()
        {
            return View();
        }

        public IActionResult Delete(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteBookFromShoppingCart(userId, id);
            if (result)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
        }

        public Boolean Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.order(userId);

            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51PYQudRwVLkNXMp5Hqe86wlHMrrHdaoMD83lWygP6tDEt1hOCF3kslT9HHdYlp4bHLNbMpVGewfc7sx4WmHafe8O002bsDIfOZ";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(order.TotalPrice) * 100,
                Description = "Book Application Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else {
                return RedirectToAction("UnsuccessfulPayment");
            }
        }

    }

   
}
