using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Models.EntityModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public List<BookCartViewModel> Books { get; set; }
        public int TotalPrice { get; set; }
    }
}