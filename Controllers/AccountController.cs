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

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, Id = model.Id.ToString() };

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
        // Get all user data
        var user = await _userManager.GetUserAsync(User);
        return View(new RegisterViewModel 
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Image = user.Image,
            PhoneNumber = user.PhoneNumber
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Profile(RegisterViewModel profile)
    {
        var user = await _userManager.GetUserAsync(User);

        // Update all properties
        user.Email = profile.Email;
        user.FirstName = profile.FirstName;
        user.LastName = profile.LastName;
        user.Address = profile.Address;
        user.PhoneNumber = profile.PhoneNumber;
        user.Image = profile.Image;

        await _userManager.UpdateAsync(user);

        return View(profile);
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