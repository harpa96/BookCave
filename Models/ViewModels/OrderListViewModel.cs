namespace BookCave.Models.EntityModels
{
    public class OrderListViewModel
    {
        public string BookName {get; set; }
        public string AutorName {get; set;}
        public int Copies {get; set;}
        public int Price {get; set;}
        public string Image {get; set;}
        public int totalPrice { get; set; }
    }
}