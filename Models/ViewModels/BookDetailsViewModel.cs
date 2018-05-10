using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public string Genre {get; set;}
        public string Description {get; set;}
        public List<Comment> Comments {get; set;}
        public List<float> Rates {get; set;}
        public float Rating {get; set;}
        public int Copies {get; set;}
    }
}
