namespace BookCave.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int AuthorID { get; set; }
        public string Genre {get; set;}

        public string Description {get; set;}
    }
}