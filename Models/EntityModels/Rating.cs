namespace BookCave.Models.EntityModels
{
    public class Rating
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public float Rate { get; set; }
    }
}