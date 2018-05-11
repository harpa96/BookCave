using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Http;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage= "Netfang vantar")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage= "Fornafn vantar")]
        public string FirstName { get; set; }
        [Required(ErrorMessage= "Eftirnafn vantar")]
        public string LastName { get; set; }
        [Required(ErrorMessage= "Lykilorð vantar")]
        public string Password { get; set; }
        [Required(ErrorMessage= "Heimilisfang vantar")]
        public string Address { get; set; }
        [Required(ErrorMessage= "Borg/bær vantar")]
        public string City { get; set; }
        [Required(ErrorMessage= "Póstnúmer vantar")]
        public string ZIP { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

    }
}