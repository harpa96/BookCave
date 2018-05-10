using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Http;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage= "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage= "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage= "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage= "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage= "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage= "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage= "Zip is required")]
        public string ZIP { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

    }
}