namespace BookCave.Models.InputModels
{
    public class RatingInputModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public float Rate { get; set; }
    }
}