﻿using System;
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

        public IActionResult reviewOrder()
        {
            var books = _bookService.getBooksInCart();
            return View(books);
        }

/* 
        public IActionResult Edit()
        {
            return View();
        }


        /*public IActionResult Cart(List<BookDetailsViewModel> booksInCart)

        */

        public IActionResult Cart()

        {
            
            Console.WriteLine("Fjöldi bóka þegar ég er í Cart Viewinu: " + booksInCart.Count);
            
            return View(booksInCart);
        }
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
            
            _donateService.SendDonateEmail(donate);
            //processContact() imitates a database connection
            //this will fail if ddata is not valid within contactInput
            _donateService.ProcessDonate(donate);
            
            return View();
        }

        [HttpPost]
         public IActionResult SpecialOrd(SpecialOrderInputModel specialorder)
        {
            //processContact() imitates a database connection
            //this will fail if ddata is not valid within contactInput
            _specialOrderService.ProcessSpecialOrder(specialorder);
            
            return View();
        }

        
    }
}
