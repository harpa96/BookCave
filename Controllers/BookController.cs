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

namespace BookCave.HomeController
{
    public class BookController : Controller
    {
        private BookService _bookService;

        private int currentBook;
        
        public BookController()
        {
            _bookService = new BookService();
            currentBook = 0;
        }
        
        public IActionResult Top10()
        {
            var books = _bookService.GetTop();
            
            return View(books);
        }

       /* public IActionResult getJsonData()
        {
            return Json("Hello world");
         } IF WE NEED AJAX - HARPA*/

        public IActionResult Category(int? Id, string orderby)
        {
            if (Id == 0)
            {
                var books = _bookService.GetAllBooks();
                ViewBag.Genre = "Allar bækur";

                if(orderby != null)
                {
                    books = _bookService.OrderBooks(orderby, books);
                    return View(books);
                }

                return View(books);                
            }
            
            else 
            {
                var books = _bookService.FilterCategories(Id);
                
                if(orderby != null)
                {
                    books = _bookService.OrderBooks(orderby, books);
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

        [HttpPost]
        public IActionResult Details (int? Id, BookDetailsViewModel book)
        {
            var newBook = _bookService.FindBookById(Id);
            newBook.Copies = book.Copies;

            _bookService.addToCart(newBook);

            Console.WriteLine("Bækur í körfu áður en ég færi mig um controller: " + _bookService.getBooksInCart().Count);
            return RedirectToAction("Cart");
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
            
            currentBook = (int)Id;
            ViewBag.NameOfBook = _bookService.getNameOfBook(currentBook);
        
            _bookService.AddRating(newComment.Rating, currentBook);
            
            if(newComment.Text != String.Empty)
            {
                _bookService.AddComment(newComment.Text, currentBook);
            }
            
            return RedirectToAction("Details", new {Id = currentBook});
        }
        public IActionResult Filter(string filterChoice = "", string searchTerm = "")
        {
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }

        public IActionResult Cart()
        {
            var books = _bookService.getBooksInCart();

            Console.WriteLine("Fjöldi í körfu inn í Cart viewi: " + books.Count);

            return View(books);
        }

    }
}
