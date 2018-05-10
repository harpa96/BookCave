using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public List<BookCartViewModel> Books { get; set; }
        public int Total { get; set; }
        public int TotalPlus {get; set;}
        
    }
}