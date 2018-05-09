using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class FrontPageViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Date {get; set;}
        public float Rating {get; set; }

    }
}