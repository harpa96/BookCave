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
using BookCave.Models.InputModels;
using System.Net.Mail;
using System.Net;

namespace BookCave.Controllers.HomeController
{    
    public class HomeController : Controller
    {
        private BookService _bookService;
        List<BookDetailsViewModel> myCart;
        private readonly IDonateService _donateService;

        private readonly ISpecialOrderService _specialOrderService;


        public HomeController(IDonateService donateService, ISpecialOrderService specialOrderService)
        {
            _bookService = new BookService();

            myCart = new List<BookDetailsViewModel>();

            _donateService = donateService;

            _specialOrderService = specialOrderService;

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
        public IActionResult ConfirmationSpecialOrder() 
        {
            return View();
        }
        public IActionResult ConfirmationDonate() 
        {
            return View();
        }

        public IActionResult reviewOrder()
        {
           //var books = _bookService.getBooksInCart();
            return View();
        }

/* 
        public IActionResult Edit()
        {
            return View();
        }
*/


        public IActionResult SendEmail()
        {
            return View();
        }
        public IActionResult form()
        {
            return View("form.php");
        }
        [HttpPost]
        public IActionResult Donate(DonateInputModel donate)
        {
            ViewData["ErrorMessage"] = "";
            _donateService.SendDonateEmail(donate);
            //processContact() imitates a database connection
            //this will fail if ddata is not valid within contactInput
            try {
                _donateService.ProcessDonate(donate);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Checked is missing";
                return View();
            }
            
            
            return RedirectToAction("ConfirmationDonate");
        }

        [HttpPost]
         public IActionResult SpecialOrd(SpecialOrderInputModel specialorder)
        {
            _specialOrderService.SendSpecialOrderEmail(specialorder);
            //processContact() imitates a database connection
            //this will fail if ddata is not valid within contactInput
            _specialOrderService.ProcessSpecialOrder(specialorder);
            return RedirectToAction("ConfirmationSpecialOrder");
        }

        
    }
}
