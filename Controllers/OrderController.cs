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
        
        public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            
            var orderHistory = _orderService.GetOrdersForUser(id);
            
            return View(orderHistory);
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
                CountryId = user.CountryId,
                Email = user.Email
            };
            
            
            return View(new CheckoutViewModel
            {
                User = theUser
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
                PayerCountryId = checkout.PayerCountryId,
                PayerEmail = checkout.PayerEmail
            };

            return RedirectToAction("ReviewOrder", newModel);
        }

        public IActionResult Confirmation() 
        {
            return View();
        }

 
        public IActionResult Review(CheckoutViewModel order)
        {
            _orderService.SendOrderEmail(order);
            
            return RedirectToAction("Confirmation");
        }

       
        [Authorize]
        public async Task<IActionResult> ReviewOrder(CheckoutViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var id = user.Id;

            var cart = new CartViewModel
            {
                Books = _shoppingService.GetCart(id)
            };

            var total = 0;

            foreach(var book in cart.Books)
            {
                total += book.Price*book.Copies;
            }

            cart.Total = total;
            
            if(total > 0)
            {
                cart.TotalPlus = total+500;
            }
            else
            {
                cart.TotalPlus = 0;
            }

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
                PayerCountry = model.PayerCountry,
                PayerEmail = model.PayerEmail                
            };

            return View(new ReviewOrderViewModel
            {
                 Cart = cart,
                 Checkout = checkout
            });
        }

    }
}