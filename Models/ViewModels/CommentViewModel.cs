namespace BookCave.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string BookName { get; set;}

        public string UserName { get; set; }

        public int? Rating { get; set; }

        public string Text { get; set; }
    }
}