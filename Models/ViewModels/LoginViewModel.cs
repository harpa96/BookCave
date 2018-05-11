using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression("/^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$/", ErrorMessage = "Email has to be on the format example@example.example")]
        [Required(ErrorMessage= "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage= "Password is required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}