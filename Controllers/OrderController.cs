using BookCave.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        private BookService _bookService;
        public OrderController()
        {
            _bookService = new BookService();
        }
        
        public IActionResult OrderHistory()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }
    }
}