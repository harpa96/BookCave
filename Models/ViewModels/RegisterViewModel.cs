using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZIP { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

    }
}