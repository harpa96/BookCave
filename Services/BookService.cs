using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;


namespace BookCave.Services
{
    public class BookService
    {
        private DataContext db;

        public BookService()
        {
            db = new DataContext();
        }
        
        

        public void AddRating(float rating, int book)
        {
            var rate = new Rating(){ BookId = book, Rate = rating };
            db.Add(rate);
            db.SaveChanges();
        }

        public void UpdateRating(int ratingId, float newRating) 
        {
            var rate = db.Ratings.FirstOrDefault(r => r.Id == ratingId);
            rate.Rate = newRating;
            db.Update(rate);
            db.SaveChanges();
        }

        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from b in db.Books
                        orderby b.Date descending
                        select new BookListViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Image = b.Image,
                            Price = b.Price,
                            Genre = b.Genre,
                            Date = b.Date
                        }).ToList();
                     
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

        public List<BookListViewModel> GetTop()
        { 
            var topBooks = (from b in db.Books
                        select new BookListViewModel
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Price = b.Price,
                            AuthorId = b.AuthorId,
                            Genre = b.Genre, // (from r in db.Ratings where r.BookId == b.Id select r.Rate.Average())
                            Image = b.Image,
                            Rating = (from r in db.Ratings where r.BookId == b.Id select r.Rate).ToList()
                        }).ToList();
            var returnValue = (from b in topBooks
                            orderby b.Rating.DefaultIfEmpty(0).Average() descending
                            select b).Take(10).ToList();
                
            return returnValue;
        }
        public List<BookListViewModel> FilterCategories(string category)
        {
            var allBooks = (from b in db.Books
            select new BookListViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                AuthorId = b.AuthorId,
                Genre = b.Genre,
                Image = b.Image
            }).ToList();
            
            if(category == null || category == "allar")
            {
                return allBooks;
            }


            var books = (from b in allBooks
                        where (b.Genre).ToLower() == category.ToLower()
                        select b).ToList();

            return books;
        }
        public BookDetailsViewModel FindBookById (int? Id)
        {
            var rating = (from r in db.Ratings
                                        where r.BookId == Id
                                        select r.Rate).ToList();
            
            var book = (from b in db.Books
                        where b.Id == Id
                        select new BookDetailsViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Genre = b.Genre,
                            Price = b.Price, 
                            AuthorId = b.AuthorId, 
                            Image = b.Image,
                            Description = b.Description,
                            Rating = rating.Average()
                        }).SingleOrDefault(); 
            return book;
        }

        public List<BookListViewModel> OrderBooks(string order)
        {
            if(order == "highest")
            {
                var orderedBooks = (from b in db.Books
                                    orderby b.Price descending
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        AuthorId = b.AuthorId,
                                        Genre = b.Genre
                                    }).ToList();
                return orderedBooks;
            }
            else if( order == "lowest")
            {
                var orderedBooks = (from b in db.Books
                                    orderby b.Price
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        AuthorId = b.AuthorId,
                                        Genre = b.Genre
                                    }).ToList();
                return orderedBooks;
            }
            else
            {
                var orderedBooks = (from b in db.Books
                                    orderby b.Name
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        AuthorId = b.AuthorId,
                                        Genre = b.Genre
                                    }).ToList();
                return orderedBooks;
            }            
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
                                    Price = b.Price,
                                    AuthorId = b.AuthorId,
                                    Genre = b.Genre
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

        public List<BookListViewModel> FilterBooks(string filterChoice)
        {
                var filteredBooks = (from b in db.Books
                                join a in db.Authors on b.AuthorId equals a.Id
                                where b.Genre.ToLower().Contains(filterChoice.ToLower()) || b.Language.ToLower().Contains(filterChoice.ToLower())
                                select new BookListViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Price = b.Price
                                }).ToList();
                return filteredBooks;

        }
    }
}