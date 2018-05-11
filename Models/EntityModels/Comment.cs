namespace BookCave.Models.EntityModels
{
    public class Comment
    {
        public int Id {get; set; }
        public string UserName{ get; set; }

        public int BookId { get; set; }
        
        public string Text { get; set; }
    
    }
}