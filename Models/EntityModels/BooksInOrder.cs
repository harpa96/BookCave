namespace BookCave.Models.EntityModels
{
    public class BooksInOrder
    {
        public int Id {get; set; }
        public string OrderId {get; set; }
        public int BookId {get; set;}
        public int Copies {get; set;}
    }
}