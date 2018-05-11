using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.ViewModels;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Identity;

namespace BookCave.Controllers
{
    public class CartController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private ShoppingCartService _shoppingCart;
        private BookService _bookService;

        
        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _shoppingCart = new ShoppingCartService();
            _bookService = new BookService();

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            var cart = new CartViewModel{Books = _shoppingCart.GetCart(id)};
            
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
            
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(CartViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            if(model.BookToDelete == 0)
            {
                _shoppingCart.clearCart(id);
                return RedirectToAction("Index");
            }
    
            var book = _bookService.FindBookById(model.BookToDelete);
           _shoppingCart.removeFromCart(book, id);

            return RedirectToAction("Index");
        }
    }
}