using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public List<BookCartViewModel> Books { get; set; }
        public double Total { get; set; }
        public double TotalPlus {get; set;}
        public int BookToDelete { get; set; }
        public string Discount {get;set;}        
    }
}