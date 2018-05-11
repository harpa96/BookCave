using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage="Nafn vantar")]
        public string ReceiverName { get; set; }
        [Required(ErrorMessage="Símanúmer vantar")]
        public string ReceiverPhoneNumber { get; set; }
        [Required(ErrorMessage="Heimilisfang vantar")]
        public string ReceiverAddress { get; set; }
        [Required(ErrorMessage="Borg/bæ vantar")]
        public string ReceiverCity { get; set; }
        
        public string ReceiverCountry { get; set; }
        
        public int ReceiverCountryId { get; set; }
        [Required(ErrorMessage="Póstnúmer vantar")]
        public string ReceiverZIP { get; set; }
        [Required(ErrorMessage="Nafn greiðanda vantar")]
        public string PayerName { get; set; }
        [Required(ErrorMessage="Símanúmer greiðanda vantar")]
        public string PayerPhoneNumber { get; set; }
        [Required(ErrorMessage="Heimilisfang greiðanda vantar")]
        public string PayerAddress { get; set; }
        [Required(ErrorMessage="Borg/bær greiðanda vantar")]
        public string PayerCity { get; set; }
        
        public string PayerCountry { get; set; }
        public int PayerCountryId { get; set; }
        [Required(ErrorMessage="Póstnúmer greiðanda vantar")]
        public string PayerZIP { get; set; }
        [Required(ErrorMessage="Netfang greiðanda vantar")]
        public string PayerEmail { get; set; }
        public ProfileViewModel User { get; set; }

    }
}