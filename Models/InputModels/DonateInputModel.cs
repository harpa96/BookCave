using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class DonateInputModel
    {
        [Required(ErrorMessage= "Nafn vantar")]
        public string Name { get; set; }
        [Required(ErrorMessage= "Netfang vantar")]
        public string Email { get; set; }
        [Required(ErrorMessage= "Fjöldi vantar")]
        public string Amount { get; set; }
        [Required(ErrorMessage= "Vantar samþykki")]
        public bool Checked { get; set; }
       
    }
}