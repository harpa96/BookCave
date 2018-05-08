namespace BookCave.Models.EntityModels
{
    public class OrderedBooks
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Copies { get; set; }
    }
}