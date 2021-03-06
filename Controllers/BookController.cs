using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;

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
            return View();
        }

    }
}
