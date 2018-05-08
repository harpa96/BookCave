namespace BookCave.Models.EntityModels
{
    public class OrderViewModel
    {
        public string BookName {get; set; }
        public string AutorName {get; set;}
        public int Copies {get; set;}
        public string Price {get; set;}
        public string Image {get; set;}
        public int totalPrice { get; set; }
    }
}