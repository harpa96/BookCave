using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class FrontPageListsViewModel
    {
        public List<FrontPageViewModel> PopularBooks { get; set; }
        public List<FrontPageViewModel> NewBooks { get; set; }
    }
}