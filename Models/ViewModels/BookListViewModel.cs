namespace BookCave.Models.ViewModels
{
    public class BookListViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int AuthorID { get; set; }
        public string Genre {get; set;}
        public float Rating {get; set;}
        public int Date {get; set;}
    }
}