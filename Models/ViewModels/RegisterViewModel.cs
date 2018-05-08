using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookCave.Models.ViewModels;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
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
        public string PhoneNumber { get; set; }
        
        public string Image { get; set; }

        public List<BookListViewModel> FavoriteBooks { get; set; }
    }
}