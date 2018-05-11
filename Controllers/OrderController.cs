using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private BookService _bookService;
        private OrderService _orderService;
        private ShoppingCartService _shoppingService;

        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _bookService = new BookService();
            _orderService = new OrderService();
            _userManager = userManager;
            _shoppingService = new ShoppingCartService();

        }
        
        public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            
            var orderHistory = _orderService.GetOrdersForUser(id);
            
            return View(orderHistory);
        }

        [HttpGet]
        public async Task<IActionResult> Pay() 
        {
            var user = await _userManager.GetUserAsync(User);
            
            var country = _orderService.GetCountry(user.CountryId);

            var theUser = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                ZIP = user.ZIP,
                CountryId = user.CountryId
            };
            
            return View(new CheckoutViewModel
            {
                 User = theUser
            });
        }

        [HttpPost]
        public IActionResult Pay(CheckoutViewModel model)
        {
            var reciever = new CheckoutPersonViewModel
            {
                Name = model.Reciever.Name,
                PhoneNumber = model.Reciever.PhoneNumber,
                Address = model.Reciever.Address,
                City = model.Reciever.City,
                ZIP = model.Reciever.ZIP,
                CountryId = model.Reciever.CountryId
            };

            var payer = new CheckoutPersonViewModel
            {
                Name = model.Payer.Name,
                PhoneNumber = model.Payer.PhoneNumber,
                Address = model.Payer.Address,
                City = model.Payer.City,
                ZIP = model.Payer.ZIP,
                CountryId = model.Payer.CountryId
            };
            
            return RedirectToAction("ReviewOrder");
        }

/*
        [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel profile)
    {
        var user = await _userManager.GetUserAsync(User);

        // Update all properties
        user.FirstName = profile.FirstName;
        user.LastName = profile.LastName;
        user.Address = profile.Address;
        user.ZIP = profile.ZIP;
        user.City = profile.City;
        user.CountryId = profile.CountryId;
        user.PhoneNumber = profile.PhoneNumber;
        user.Image = profile.Image;
        user.FavoriteBook = profile.FavoriteBook;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Profile");
    }
    */

    /* 
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);

        var country = (from c in db.Countries 
                        where user.CountryId == c.Id
                        select c.Name).ToString();

        return View(new ProfileViewModel 
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            ZIP = user.ZIP,
            Country = country,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber,
            FavoriteBook = user.FavoriteBook
        });
    }
    */

         public IActionResult Confirmation() 
        {
            return View();
        }

        public async Task<IActionResult> ReviewOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            
            var id = user.Id;
            var cart = new CartViewModel
            {
                Books = _shoppingService.GetCart(id)
            };
            var country = _orderService.GetCountry(user.CountryId);

            var theUser = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                ZIP = user.ZIP,
                CountryId = user.CountryId
            };
            
            return View(new CheckoutViewModel
            {
                 User = theUser
            });
        }

    }
}