using System.Collections.Generic;

namespace BookCave.Models.EntityModels
{
    public class Rates
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public float Average {get; set; }
    }
}