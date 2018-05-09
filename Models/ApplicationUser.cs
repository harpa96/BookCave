using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookCave.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public int CountryId { get; set; }
        public string Image { get; set; }
        public string FavoriteBook { get; set; }

    }
}