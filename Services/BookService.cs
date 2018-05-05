using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;


namespace BookCave.Services
{
    public class BookService
    {
        private DataContext db;

        public BookService()
        {
            db = new DataContext();
        }
        
        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from b in db.Books
                        orderby b.Price descending
                        select new BookListViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Image = b.Image,
                            Price = b.Price
<<<<<<< HEAD
                        }).Take(6).ToList();
=======
                        }).ToList();
>>>>>>> bc4d6dfdf9a25c9137e21d90b4984f1ffbd8baf1
            return books;
        }
 
        public List<AuthorListViewModel> GetAllAuthors()
        {
            var authors = (from a in db.Authors
                        select new AuthorListViewModel
                        {
                            Id = a.Id, 
                            Name = a.Name
                        }).ToList();
            return authors;
        }

    }
}