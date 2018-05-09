using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class BookListViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }

        public int AuthorId {get; set;}
        public int AuthorName { get; set; }
        public string Genre {get; set; }
        public List<float> Rating {get; set; }
        public int Date {get; set;}
      
    }
}