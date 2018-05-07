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
        private int? currentBook;
        public BookController()
        {
            _bookService = new BookService();
            books = _bookService.GetAllBooks();
            currentBook = 0;
        }
        
        public IActionResult Category(string Id)
        {
            books = _bookService.FilterCategories(Id);
           
            if(Id == "allar")
            {
                ViewBag.Genre = "Allar bækur";
            }

            else if(Id == "top10")
            {
                ViewBag.Genre = "Topp 10 listinn";
            }

            else
            {
                ViewBag.Genre = (books[0].Genre).ToUpper();

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
            currentBook = Id;

            return View(book);
        }

       /* [HttpPost]

        public IActionResult Details(RatingInputModel rating)
        {
            _bookService.AddRating(rating.Rate, currentBook);
            
            return RedirectToAction("");
        }*/

        public IActionResult Filter(string filterChoice)
        {
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }

    }
}
