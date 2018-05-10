using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class DonateInputModel
    {
        [Required(ErrorMessage= "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage= "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage= "Amount is required")]
        public string Amount { get; set; }

        [Required(ErrorMessage= "Checked is required")]

        public bool IsTrue => true;

        [Required]
        [Display(Name = "I agree to the terms and conditions")]
        [Compare(nameof(IsTrue), ErrorMessage = "Please agree to Terms and Conditions")]
        public bool Checked { get; set; }
       
    }
}