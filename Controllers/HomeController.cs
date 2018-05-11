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
        private readonly IDonateService _donateService;
        private readonly ISpecialOrderService _specialOrderService;


        public HomeController(IDonateService donateService, ISpecialOrderService specialOrderService)
        {
            _bookService = new BookService();
            _donateService = donateService;
            _specialOrderService = specialOrderService;
        }
        
        
        [HttpGet]
        //Birtir forsíðu
        public IActionResult Index()
        {
            var books = _bookService.GetAllFrontPageBooks();
            return View(books);
        }

        [HttpGet]
        //Birtir leitarniðurstöður úr searchbar
        public IActionResult SearchBar(string searchInput)
        {
            if (!string.IsNullOrEmpty(searchInput))
            {
                var searchedBooks = _bookService.SearchedBooks(searchInput);
                return View(searchedBooks);
            }
            
            else
            {
                return View("Category", "Book");
            }
        }

        
        [HttpGet]
        public IActionResult Donate()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SpecialOrd() 
        {
            return View();
        }


        [HttpGet]
        public IActionResult About() 
        {
            return View();
        }


        [HttpGet]
        public IActionResult OurTeam()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Terms()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConfirmationSpecialOrder() 
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult ConfirmationDonate() 
        {
            return View();
        }


        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult Donate(DonateInputModel donate)
        {
            ViewData["ErrorMessage"] = "";
            _donateService.SendDonateEmail(donate);
            
            //processContact() imitates a database connection
            //this will fail if ddata is not valid within contactInput
            try 
            {
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
         public IActionResult SpecialOrd(SpecialOrderInputModel specialOrder)
        {
            _specialOrderService.SendSpecialOrderEmail(specialOrder);
            
            //processContact() imitates a database connection
            //this will fail if data is not valid within contactInput
            _specialOrderService.ProcessSpecialOrder(specialOrder);

            return RedirectToAction("ConfirmationSpecialOrder");
        }        
    }
}
