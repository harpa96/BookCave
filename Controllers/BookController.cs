using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.ViewModels;

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
        
        public IActionResult Category(string Id)
        {
            books = _bookService.FilterCategories(Id);
           
            if(Id == "allar")
            {
                ViewBag.Genre = "Allar bækur";
            }
            else
            {
                ViewBag.Genre = (books[0].Genre).ToUpper();
            }

            return View(books);
        }
        
        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return View("NotFound");
            }
            
            var book = _bookService.FindBookById(Id);

            return View(book);
        }

        public IActionResult Filter(string filterChoice)
        {
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }

    }
}
