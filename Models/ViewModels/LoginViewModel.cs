using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class LoginViewModel
    {
        [RegularExpression( "^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$" , ErrorMessage = "netfang ekki rétt slegið inn, passaðu að það sé á forminu someone@example.com" )]
        [Required(ErrorMessage= "Netfang vantar")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Lykilorð verður að vera a.m.k 8 stafir að lengd, með hástaf og tölustaf", MinimumLength = 8)]
        [Required(ErrorMessage= "Lykilorð vantar")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}