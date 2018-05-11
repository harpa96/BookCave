namespace BookCave.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public string ReceiverName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }
        public int ReceiverCountryId { get; set; }
        public string ReceiverZIP { get; set; }
        public string PayerName { get; set; }
        public string PayerPhoneNumber { get; set; }
        public string PayerAddress { get; set; }
        public string PayerCity { get; set; }
        public string PayerCountry { get; set; }
        public int PayerCountryId { get; set; }
        public string PayerZIP { get; set; }
        public string PayerEmail { get; set; }
        public ProfileViewModel User { get; set; }

    }
}