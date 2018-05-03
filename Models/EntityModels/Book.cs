namespace BookCave.Models.EntityModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AuthorId {get; set;}
        
        public int Date {get; set;}

        public string Genre {get; set;}

        public string Language {get; set;}

        public float Rating {get; set;}

        public int Price {get; set;}

        public string Image {get; set;}

        public string Description {get; set;}
    
    }
}