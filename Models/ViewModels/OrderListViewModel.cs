using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderListViewModel
    {
        public string userId { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}