namespace BookCave.Models.EntityModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Copies { get; set; }
    }
}