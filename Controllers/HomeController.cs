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
    public class HomeController : Controller
    {
        private BookService _bookService;

        public HomeController()
        {
            _bookService = new BookService();
        }
        
        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        public IActionResult SearchBar(string searchInput)
        {
            var searchedBooks = _bookService.SearchedBooks(searchInput);
            return View(searchedBooks);
        }

        
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Donate()
        {
            return View();
        }

        public IActionResult SpecialOrd() 
        {
            return View();
        }

        public IActionResult About() 
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Login() 
        {
            return View();
        }
         public IActionResult Pay() 
        {
            return View();
        }

         public IActionResult Confirmation() 
        {
            return View();
        }
    }
}
