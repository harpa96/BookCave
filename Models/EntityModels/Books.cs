namespace BookCave.Models.EntityModels
{
    public class Books
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public int AuthorId {get; set;}
        public int Date {get; set;}
        public int GenreId {get; set;}
        public string Language {get; set;}
        public int Price {get; set;}
        public string Image {get; set;}
        public string Description {get; set;}
    }
}