namespace BookCave.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserName { get; set; }

        public int? Rating { get; set; }

        public string Text { get; set; }
    }
}