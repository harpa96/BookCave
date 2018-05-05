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
                        }).Take(12).ToList();
                     
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

        public List<BookListViewModel> SearchedBooks(string searchInput)
        {

            if (searchInput != null)
            {
                var searchedBooks = (from b in db.Books
                                join a in db.Authors on b.AuthorId equals a.Id
                                where b.Name.ToLower().Contains(searchInput.ToLower()) || a.Name.ToLower().Contains(searchInput.ToLower())
                                select new BookListViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Price = b.Price
                                }).ToList();
                return searchedBooks;
            }

            var books = (from b in db.Books
                         select new BookListViewModel
                         {
                            Id = b.Id, 
                            Name = b.Name,
                            Image = b.Image,
                            Price = b.Price
                         }).ToList();
            return books;
        }

    }
}