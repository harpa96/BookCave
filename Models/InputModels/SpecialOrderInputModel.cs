
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class SpecialOrderInputModel
    {
        [Required(ErrorMessage= "Nafn vantar")]
        public string Name { get; set; }
        [Required(ErrorMessage= "Símanúmer vantar")]
        public string Phone { get; set; }
        [Required(ErrorMessage= "Heimilisfang vantar")]
        public string Address { get; set; }
        [Required(ErrorMessage= "Póstnúmer vantar")]
        public string Zip { get; set; }
        [Required(ErrorMessage= "Borg/bær vantar")]
        public string City { get; set; }

        [Required(ErrorMessage= "Netfang vantar")]
        public string Email { get; set; }
       
        [Required(ErrorMessage= "Titill vantar")]
        public string Title { get; set; }

        [Required(ErrorMessage= "Höfund vantar")]
        public string Author { get; set; }

        [Required(ErrorMessage= "Útgáfuár vantar")]
        public string PublishDate { get; set; }

        [Required(ErrorMessage= "Fjölda vantar")]
        public int Amount { get; set; }
    }
}