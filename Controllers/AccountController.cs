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

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, Address = model.Address, Image = model.Image, FirstName = model.FirstName, LastName = model.LastName, ZIP = model.ZIP, CountryId = model.CountryId, City = model.City};

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
        return View(new ProfileViewModel 
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            ZIP = user.ZIP,
            CountryId = user.CountryId,
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
        return View(new ProfileViewModel 
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            ZIP = user.ZIP,
            CountryId = user.CountryId,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber,
            FavoriteBook = user.FavoriteBook
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
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

    public IActionResult AccessDenied()
    {
        return View();
    }

}