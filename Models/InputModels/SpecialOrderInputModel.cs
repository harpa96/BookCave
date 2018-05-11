
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class SpecialOrderInputModel
    {
        [Required(ErrorMessage= "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage= "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage= "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage= "Zip is required")]
        public string Zip { get; set; }
        [Required(ErrorMessage= "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage= "Email is required")]
        public string Email { get; set; }
       
        [Required(ErrorMessage= "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage= "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage= "PublishDate is required")]
        public string PublishDate { get; set; }

        [Required(ErrorMessage= "Amount is required")]
        public int Amount { get; set; }
    }
}