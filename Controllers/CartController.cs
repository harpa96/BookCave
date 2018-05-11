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
            var userId = user.Id;
            var cart = new CartViewModel{Books = _shoppingCart.GetCart(userId)};
           
            cart.Total = _shoppingCart.GetTotal(userId);
            
            //Ef það eru einhverjar vörur í körfunni þá bætist sendingarkostnaður við gjaldið, annars ekki
            if (cart.Total > 0)
            {
                cart.TotalPlus = cart.Total + 500;
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
            var userId = user.Id;
                      
            //Ef notandi vill hreinsa körfuna þá er er ekkert bookId (BookToDelete == 0)
            if (model.BookToDelete == 0)
            {
                _shoppingCart.ClearCart(userId);
                return RedirectToAction("Index");
            }

            //Eyði einu eintaki af ákveðinni tegund
            var book = _bookService.FindBookById(model.BookToDelete);
            _shoppingCart.removeFromCart(book, userId);
        
            return RedirectToAction("Index");
        }
        
    }
}