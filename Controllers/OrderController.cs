using System;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private BookService _bookService;
        private OrderService _orderService;
        private ShoppingCartService _shoppingService;
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _orderService = new OrderService();
            _userManager = userManager;
            _shoppingService = new ShoppingCartService();
        }
        
        public IActionResult OrderHistory()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Pay() 
        {
            var user = await _userManager.GetUserAsync(User);
            
            var country = _orderService.GetCountry(user.CountryId);

            var theUser = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                ZIP = user.ZIP,
                CountryId = user.CountryId
            };
            
            Console.WriteLine("FER INN HER");
            return View(new CheckoutViewModel
            {
                User = theUser
                //PayerName = theUser.FirstName + " " + theUser.LastName
               /* ReceiverPhoneNumber = checkout.ReceiverPhoneNumber,
                ReceiverAddress = checkout.ReceiverAddress,
                ReceiverCity = checkout.ReceiverCity,
                ReceiverZIP = checkout.ReceiverZIP,
                ReceiverCountryId = checkout.ReceiverCountryId,
                PayerName = checkout.PayerName,
                PayerPhoneNumber = checkout.PayerPhoneNumber,
                PayerAddress = checkout.PayerAddress,
                PayerCity = checkout.PayerCity,
                PayerZIP = checkout.PayerZIP,
                PayerCountryId = checkout.PayerCountryId*/
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Pay(CheckoutViewModel checkout)
        {         
            var newModel = new CheckoutViewModel
            {
                ReceiverName = checkout.ReceiverName,
                ReceiverPhoneNumber = checkout.ReceiverPhoneNumber,
                ReceiverAddress = checkout.ReceiverAddress,
                ReceiverCity = checkout.ReceiverCity,
                ReceiverZIP = checkout.ReceiverZIP,
                ReceiverCountryId = checkout.ReceiverCountryId,
                PayerName = checkout.PayerName,
                PayerPhoneNumber = checkout.PayerPhoneNumber,
                PayerAddress = checkout.PayerAddress,
                PayerCity = checkout.PayerCity,
                PayerZIP = checkout.PayerZIP,
                PayerCountryId = checkout.PayerCountryId
            };

            Console.WriteLine("PAYER NAME IN Í POST PAY: " + newModel.PayerName);

            return RedirectToAction("ReviewOrder", new {model = newModel});
        }

         public IActionResult Confirmation() 
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ReviewOrder(CheckoutViewModel model)
        {
            Console.WriteLine("NAFN PAYER: " + model.PayerName);
            var user = await _userManager.GetUserAsync(User);
            
            var id = user.Id;

            var cart = new CartViewModel
            {
                Books = _shoppingService.GetCart(id),
                Total = 0,
                TotalPlus = 0,
                BookToDelete = 0
            };

            var country = _orderService.GetCountry(user.CountryId);

            var checkout = new CheckoutViewModel
            {
                ReceiverName = model.ReceiverName,
                ReceiverPhoneNumber = model.ReceiverPhoneNumber,
                ReceiverAddress = model.ReceiverAddress,
                ReceiverCity = model.ReceiverCity,
                ReceiverZIP = model.ReceiverZIP,
                ReceiverCountry = model.ReceiverCountry,        
                PayerName = model.PayerName,
                PayerPhoneNumber = model.PayerPhoneNumber,
                PayerAddress = model.PayerAddress,
                PayerCity = model.PayerCity,
                PayerZIP = model.PayerZIP,
                PayerCountry = model.PayerCountry                
            };

            return View(new ReviewOrderViewModel
            {
                 Cart = cart,
                 Checkout = checkout
            });
        }

    }
}