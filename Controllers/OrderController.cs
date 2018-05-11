using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        private BookService _bookService;
        private OrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _orderService = new OrderService();
            _userManager = userManager;
        }
        
        public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            
            var orderHistory = _orderService.GetOrdersForUser(id);
            
            return View(orderHistory);
        }

    }
}