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

        
        public CartController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _shoppingCart = new ShoppingCartService();

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            var cart = new CartViewModel{Books = _shoppingCart.getCart(id)};
            
            var total = 0;

            foreach(var book in cart.Books)
            {
                total += book.Price*book.Copies;
            }

            cart.Total = total;
            cart.TotalPlus = total+500;

            return View(cart);
        }

        /*[Authorize]
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            var cart = new CartViewModel{Books = _shoppingCart.getCart(id)};
            
            var total = 0;

            foreach(var book in cart.Books)
            {
                total += book.Price*book.Copies;
            }

            cart.Total = total;
            cart.TotalPlus = total+500;

            return View(cart);
        }*/

    }
}