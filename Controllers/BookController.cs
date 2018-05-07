using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.RecordStore.HomeController
{
       public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        public IActionResult Category()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }
        
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Filter(string filterChoice)
        {
            var filteredBooks = _bookService.SearchedBooks(filterChoice);
            return View(filteredBooks);
        }

    }
}
