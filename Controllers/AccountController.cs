using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.ViewModels;
using System.Security.Claims;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;
using BookCave.Data;




/* 
using Microsoft.AspNetCore.Http;
using System.IO;
*/


public class AccountController : Controller
{
    //private readonly IHostingEnvironment _env;
    private DataContext db;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
   // private readonly IUserService _userService; ***FYRIR ERRORHANDLING -HARPA***

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager /*,IUserService userService ***FYRIR ERRORHANDLING -HARPA*** */)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        db = new DataContext();
        //_userService = userService; ***FYRIR ERRORHANDLING -HARPA***
    }
/* 
    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel model)
    {
        var file = model.File;
        

        if (file.Length > 0)
        {
            string path = Path.Combine(_env.WebRootPath, "images");

            using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
            {
                file.CopyToAsync(fs);
            }

            model.Source = $"/images{file.FileName}";
            return Ok(model);
        }
        return BadRequest();
    }

    */

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
/* 
        var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "uploads\\img");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            model.BookPic = fileName;
                        }

                    }
                }
            }
            */
        if (String.IsNullOrEmpty(model.Image))
        {
            model.Image = "https://www.pastepic.xyz/images/2018/05/10/Screen-Shot-2018-05-10-at-23.57.06568dff321a601717.png";
        }

        var user = new ApplicationUser 
        { 
            UserName = model.Email, 
            Email = model.Email, 
            PhoneNumber = model.PhoneNumber, 
            Address = model.Address, 
            Image = model.Image,
            FirstName = model.FirstName, 
            LastName = model.LastName, 
            ZIP = model.ZIP, 
            CountryId = model.CountryId, 
            City = model.City
        };
/* 
        using (var memoryStream = new MemoryStream())
        {
            await model.AvatarImage.CopyToAsync(memoryStream);
            user.AvatarImage = memoryStream.ToArray();
        }
*/

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            //The user is successfully registered
            //Add the concatenated first and last name as fullName in claims
            await _userManager.AddClaimAsync(user, new Claim("FirstName", model.FirstName));
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        
        var country = (from c in db.Countries 
                        where user.CountryId == c.Id
                        select c.Name).FirstOrDefault().ToString();
        
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

    [HttpGet]
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

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Login(LoginViewModel model)
    {
        ViewData["ErrorMessage"] = "";

        if (!ModelState.IsValid)
        {
                ViewData["ErrorMessage"] = "Notandi finnst ekki í kerfinu";
                return View();      
        }
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    /* 
    [HttpPost("UploadFiles")]
    public async Task<IActionResult> Post(List<IFormFile> files)
    {
        long size = files.Sum(f => f.Length);

        // full path to file in temp location
        var filePath = Path.GetTempFileName();

        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
        }

        // process uploaded files
        // Don't rely on or trust the FileName property without validation.

        return Ok(new { count = files.Count, size, filePath});
    }
*/
    public IActionResult AccessDenied()
    {
        return View();
    }


}
/* 
    ***GERDI THETTA FYRIR ERRORHANDLING - HARPA*** 
    [HttpPost]
    public IActionResult Register(RegisterViewModel register)
    {
        
        //processContact() imitates a database connection
        //this will fail if ddata is not valid within contactInput
        _userService.ProcessDonate(register);
        
        return View();
    }
    */