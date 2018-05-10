using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.ViewModels;
using System.Net.Mail;
using System.Net;

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
            var books = _bookService.GetAllFrontPageBooks();
            return View(books);
        }

        public IActionResult SearchBar(string searchInput)
        {
            var searchedBooks = _bookService.SearchedBooks(searchInput);
            return View(searchedBooks);
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

         public IActionResult Pay() 
        {
            return View();
        }

         public IActionResult Confirmation() 
        {
            return View();
        }

/* 
        public IActionResult Edit()
        {
            return View();
        }

        */

        public IActionResult Cart()
        {
            var books = _bookService.getBooksInCart();
            
            return View(books);
        }
        public IActionResult SendEmail()
        {
            return View();
        }
        public IActionResult form()
        {
            return View("form.php");
        }

        
    }
}
