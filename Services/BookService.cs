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
        private List<BookDetailsViewModel> cart;

        public BookService()
        {
            db = new DataContext();
            cart = new List<BookDetailsViewModel>()
            {
                new BookDetailsViewModel(){Name = "Bókin hennar Vigdísar", Price=400, Copies=2}
            };
        }
        
        public List<BookDetailsViewModel> getBooksInCart()
        {
            return cart;
        }

        public void addToCart(BookDetailsViewModel book)
        {
            cart.Add(book);
        }

        public void AddRating(float? rating, int book)
        {
            if(rating != null)
            {
                var rate = new Rating(){ BookId = book, Rate = (float)rating};
                db.Add(rate);
                db.SaveChanges();
            }
        }

        public void AddComment(string text, int book)
        {
            if(!string.IsNullOrEmpty(text))
            {
                var comment = new Comment(){BookId = book, Text = text};
                db.Add(comment);
                db.SaveChanges();
            }
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
                            join g in db.Genre on b.GenreId equals g.Id
                            orderby b.Date descending
                            select new BookListViewModel
                            {
                                Id = b.Id, 
                                Name = b.Name,
                                Image = b.Image,
                                Price = b.Price,
                                Genre = g.TheGenre,
                                Date = b.Date
                            }).ToList();

            return books;
        }

        public FrontPageListsViewModel GetAllFrontPageBooks()
        {
            var model = new FrontPageListsViewModel();

            var newBooks = (from b in db.Books
                            join g in db.Genre on b.GenreId equals g.Id
                            orderby b.Date descending
                            select new FrontPageViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                Image = b.Image,
                                Price = b.Price,
                                Date = b.Date
                            }).Take(12).ToList();

            var popBooks = (from b in db.Books
                            join g in db.Genre on b.GenreId equals g.Id
                            join r in db.Ratings on b.Id equals r.BookId
                            orderby r.Rate descending
                            select new FrontPageViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                Image = b.Image,
                                Price = b.Price,
                                Rating = r.Rate
                            }).Take(12).ToList();

            model.NewBooks = newBooks;
            model.PopularBooks = popBooks;

            return model;
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
                        join g in db.Genre on b.GenreId equals g.Id
                        select new BookListViewModel
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Price = b.Price,
                            AuthorId = b.AuthorId,
                            Genre = g.TheGenre,
                            Image = b.Image,
                            Rating = (from r in db.Ratings where r.BookId == b.Id select r.Rate).ToList()
                        }).ToList();
                        
            var returnValue = (from b in topBooks
                            orderby b.Rating.DefaultIfEmpty(0).Average() descending
                            select b).Take(10).ToList();
            return returnValue;
        }
        public List<BookListViewModel> FilterCategories(int? Id)
        {
            if (Id == null)
            {
                return GetAllBooks();
            }
            
            var allBooks = (from b in db.Books
            join g in db.Genre on b.GenreId equals g.Id
            where b.GenreId == Id
            select new BookListViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                AuthorId = b.AuthorId,
                Genre = g.TheGenre,
                Image = b.Image
            }).ToList();
            
            if(Id == null)
            {
                return allBooks;
            }

            return allBooks;
        }
        public BookDetailsViewModel FindBookById (int? Id)
        {
            var rating = (from r in db.Ratings
                        where r.BookId == Id
                        select r.Rate).ToList();

            var comments = (from c in db.Comments
                            where c.BookId == Id
                            select c).ToList();
            
            var book = (from b in db.Books
                        join g in db.Genre on b.GenreId equals g.Id
                        where b.Id == Id
                        select new BookDetailsViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Price = b.Price, 
                            AuthorId = b.AuthorId, 
                            Image = b.Image,
                            Genre = g.TheGenre,
                            Comments = comments,
                            Description = b.Description,
                            Rating = rating.Average()
                        }).SingleOrDefault(); 
            return book;
        }

        public List<BookListViewModel> OrderBooks(string order, List<BookListViewModel> books)
        {
            if(order == "highest")
            {
                books = (from b in db.Books
                                    join g in db.Genre on b.GenreId equals g.Id
                                    orderby b.Price descending
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        Genre = g.TheGenre,
                                        AuthorId = b.AuthorId,
                                    }).ToList();
                return books;
            }
            else if( order == "lowest")
            {
                books = (from b in db.Books
                                    join g in db.Genre on b.GenreId equals g.Id
                                    orderby b.Price ascending
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        Genre = g.TheGenre,
                                        AuthorId = b.AuthorId,
                                    }).ToList();
                return books;
            }
            else
            {
                books = (from b in db.Books
                                    join g in db.Genre on b.GenreId equals g.Id
                                    orderby b.Name
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                        Genre = g.TheGenre,
                                        AuthorId = b.AuthorId
                                    }).ToList();
                return books;
            }            
        }

        public List<BookListViewModel> SearchedBooks(string searchInput)
        {

            if (searchInput != null)
            {
                var searchedBooks = (from b in db.Books
                                join g in db.Genre on b.GenreId equals g.Id
                                join a in db.Authors on b.AuthorId equals a.Id
                                where b.Name.ToLower().Contains(searchInput.ToLower()) || a.Name.ToLower().Contains(searchInput.ToLower())
                                select new BookListViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Price = b.Price,
                                    Genre = g.TheGenre,
                                    AuthorId = b.AuthorId
                                }).ToList();
                return searchedBooks;
            }

            var books = (from b in db.Books
                        join g in db.Genre on b.GenreId equals g.Id
                        select new BookListViewModel
                         {
                            Id = b.Id, 
                            Name = b.Name,
                            Image = b.Image,
                            Genre = g.TheGenre,
                            Price = b.Price
                         }).ToList();
            return books;
        }

        public List<BookListViewModel> FilterBooks(string filterChoice)
        {
                var filteredBooks = (from b in db.Books
                                join a in db.Authors on b.AuthorId equals a.Id
                                join g in db.Genre on b.GenreId equals g.Id
                                //where b.Genre.ToLower().Contains(filterChoice.ToLower()) || b.Language.ToLower().Contains(filterChoice.ToLower())
                                select new BookListViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Genre = g.TheGenre,
                                    Price = b.Price
                                }).ToList();
                return filteredBooks;

        }
        public string getGenre(int? Id)
        {
            string genre = (from g in db.Genre
                            where Id == g.Id
                            select g.TheGenre).Single();
            return genre;
        }

        public string getNameOfBook(int Id)
        {
            var name = (from b in db.Books
                        where Id == b.Id
                        select b.Name).ToString();

            return name;
        }



    }
}