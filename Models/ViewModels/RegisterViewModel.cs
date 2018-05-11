using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Http;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        
        [EmailAddress]
        [RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "netfang ekki rétt slegið inn, passaðu að það sé á forminu someone@example.com" )]
        [Required(ErrorMessage= "Netfang vantar")]
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