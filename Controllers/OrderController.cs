using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        private BookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _userManager = userManager;
        }
        
        public IActionResult OrderHistory()
        {
            
            return View();
        }

    }
}