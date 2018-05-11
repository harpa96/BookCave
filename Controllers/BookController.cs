using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.ViewModels;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Identity;

namespace BookCave.Controllers.BookController
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private ShoppingCartService _shoppingCart;

        //Notum til að fá info um current user
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _shoppingCart = new ShoppingCartService();
            _userManager = userManager;
        }
        
        public IActionResult Top10()
        {
            var books = _bookService.GetTop();
            
            return View(books);
        }
        public IActionResult Category(int? Id, string orderby)
        {
            
            if (Id == 0 || Id == null)
            {
                var books = _bookService.GetAllBooks();
                ViewBag.Genre = "Allar bækur";

                if(orderby != null)
                {
                    books = _bookService.OrderBooks(orderby, 0);
                    return View(books);
                }

                return View(books);                
            }
            
            else 
            {
                var books = _bookService.FilterCategories(Id);
                
                if(orderby != null)
                {
                    books = _bookService.OrderBooks(orderby, (int)Id);
                    return View(books);
                }
                
                ViewBag.Genre = _bookService.getGenre(Id);
                return View(books);
            }
        }
        
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return View("NotFound");
            }
            
            var book = _bookService.FindBookById(Id);

            return View(book);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details (int? Id, BookDetailsViewModel book)
        {
            var newBook = _bookService.FindBookById(Id);
            newBook.Copies = book.Copies;

            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            
            _shoppingCart.addToCart(newBook, id);
           
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult AddRating()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddRating(int? Id, CommentViewModel newComment)
        {
            if(Id == null)
            {
                return RedirectToAction("NotFound");
            }
            
            var currentBook = (int)Id;
            ViewBag.NameOfBook = _bookService.getNameOfBook(currentBook);
        
            _bookService.AddRating(newComment.Rating, currentBook);
            
            if(newComment.Text != String.Empty)
            {
                _bookService.AddComment(newComment.Text, currentBook);
            }
            
            return RedirectToAction("Details", new {Id = currentBook});
        }
        
        /*public IActionResult Filter(string filterChoice = "", string searchTerm = "")
        {
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }*/
    }
}
