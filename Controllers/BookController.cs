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
        
        [HttpGet]
        public IActionResult Top10()
        {
            var books = _bookService.GetTop();
            
            return View(books);
        }
        
        public IActionResult Category(int? Id, string orderBy)
        {
            
            //Ef id == 0 þá er notandi að reyn að fara í flokkinn allir flokkar
            //Fer líka í allir flokkar ef slóðin er skrifuð með engu route-id
            if (Id == 0 || Id == null)
            {
                var books = _bookService.GetAllBooks();
                ViewBag.Genre = "Allar bækur";

                //Ef notandi vill raða listanum í ákveðinni röð
                if (orderBy != null)
                {
                    //Seinni parameter er 0 því við viljum raða öllum bókum
                    books = _bookService.OrderBooks(orderBy, 0);
                    return View(books);
                }

                return View(books);                
            }
            
            else 
            {
                //Sía út ákveðinn flokk
                var books = _bookService.FilterCategories(Id);
                
                if (orderBy != null)
                {
                    books = _bookService.OrderBooks(orderBy, (int)Id);
                    return View(books);
                }
                
                //Sækja flokkheiti til að birta sem heading á síðunni
                ViewBag.Genre = _bookService.GetGenre(Id);

                return View(books);
            }
        }
        
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var book = _bookService.FindBookById(id);
            
            if (book == null)
            {
                return View("NotFound");
            }

            return View(book);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details (int? id, BookDetailsViewModel book)
        {
            var newBook = _bookService.FindBookById(id);
            newBook.Copies = book.Copies;

            var user = await _userManager.GetUserAsync(User);
            var newId = user.Id;
            
            _shoppingCart.AddToCart(newBook, newId);
           
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult AddRating(int id)
        {
            var book = _bookService.FindBookById(id);
            ViewBag.Title = book.Name;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRating(int? id, CommentViewModel newComment)
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = user.FirstName;
            
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            
            var currentBook = (int)id;
            ViewBag.NameOfBook = _bookService.GetNameOfBook(currentBook);
            _bookService.AddRating(newComment.Rating, currentBook);
            
            if (newComment.Text != String.Empty)
            {
                _bookService.AddComment(userName, newComment.Text, currentBook);
            }
            
            return RedirectToAction("Details", new {id = currentBook});
        }
        
    }
}
