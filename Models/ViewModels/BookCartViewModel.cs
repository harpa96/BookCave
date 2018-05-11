using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class BookCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int PriceSum { get; set; }
        public int Copies { get; set; }

        public string Author {get; set; }
    }
}