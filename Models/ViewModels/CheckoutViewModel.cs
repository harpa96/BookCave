using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage="Þarft að fylla inn nafn")]
        public string ReceiverName { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn símanúmer")]
        public string ReceiverPhoneNumber { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn heimilisfang")]
        public string ReceiverAddress { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn Borg/bæ")]
        public string ReceiverCity { get; set; }
        
        public string ReceiverCountry { get; set; }
        
        public int ReceiverCountryId { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn póstnúmer")]
        public string ReceiverZIP { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn nafn greiðanda")]
        public string PayerName { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn símanúmer greiðanda")]
        public string PayerPhoneNumber { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn heimilisfang greiðanda")]
        public string PayerAddress { get; set; }
        [Required(ErrorMessage="Þarft að fylla borg/bæ greiðanda")]
        public string PayerCity { get; set; }
        
        public string PayerCountry { get; set; }
        public int PayerCountryId { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn póstnúmer greiðanda")]
        public string PayerZIP { get; set; }
        [Required(ErrorMessage="Þarft að fylla inn netfang greiðanda")]
        public string PayerEmail { get; set; }
        public ProfileViewModel User { get; set; }

    }
}