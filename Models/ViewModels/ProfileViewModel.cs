namespace BookCave.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string FavoriteBook { get; set; }
    }
}