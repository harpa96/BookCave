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
        private List<BookListViewModel> books;

        public BookController()
        {
            _bookService = new BookService();
            books = _bookService.GetAllBooks();
            
        }
        
        public IActionResult Top10()
        {
            books = _bookService.GetTop();
            
            return View(books);
        }

       /* public IActionResult getJsonData()
        {
            return Json("Hello world");
         } IF WE NEED AJAX - HARPA*/

        public IActionResult Category(int? Id)

        {
            if (Id == null)
            {
                books = _bookService.GetAllBooks();
                ViewBag.Genre = "Allar bækur";
            }
            
            else 
            {
                books = _bookService.FilterCategories(Id);
                ViewBag.Genre = _bookService.getGenre(Id);
            }
    
            return View(books);
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

       /* [HttpPost]

        public IActionResult Details(RatingInputModel rating)
        {
            _bookService.AddRating(rating.Rate, currentBook);
            
            return RedirectToAction("");
        }*/

        public IActionResult Filter(string filterChoice = "", string searchTerm = "")
        {
            if(searchTerm != "") 
            {
                // skila leitar view
            }
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }

    }
}
