/*using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;


namespace BookCave.Services
{
    public class AuthorService
    {
        private DataContext db;

        public AuthorService()
        {
            db = new DataContext();
        }
        
        public List<BookListViewModel> GetAllBooks()
        {
            var authors = (from b in db.Books
                        select new BookListViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name
                        }).ToList();
            return authors;
        }
    }
}
*/