using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;

namespace BookCave.HomeController
{
    public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }
        [HttpGet]
        public IActionResult Category(string Id)
        {

            var filteredBooks = _bookService.FilterCategories(Id);

            if(Id == "allar")
            {
                ViewBag.Genre = "Allar bækur";
            }
            else
            {
                ViewBag.Genre = (filteredBooks[0].Genre).ToUpper();
            }

            return View(filteredBooks);
        }
        
        [HttpPost]
        public IActionResult Category()
        {
            
            
            return RedirectToAction("Category");
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
