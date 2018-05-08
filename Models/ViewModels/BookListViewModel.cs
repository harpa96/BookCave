using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class BookListViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
<<<<<<< HEAD
        public int AuthorId { get; set; }
        public string Genre {get; set; }
        public List<float> Rating {get; set; }
=======
        public int AuthorID { get; set; }
        public string Genre {get; set;}
        public int Date {get; set;}
      
>>>>>>> bddaac91295144e08dda5d7b9b90a129d6aefe69
    }
}