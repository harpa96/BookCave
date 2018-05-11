using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using System;

namespace BookCave.Services
{
    public class BookService
    {
        private DataContext db;
        private List<BookDetailsViewModel> cart;

        public BookService()
        {
            db = new DataContext();
            cart = new List<BookDetailsViewModel>();
        }

        //bætir við einkunn við eftirfarandi bók hafi notandi
        //Rating færibreytan getur verið null ef notandi bætti bara við kommenti ekki einkunn
        public void AddRating(float? rating, int book)
        {
            if(rating != null)
            {
                var rate = new Rating(){ BookId = book, Rate = (float)rating};
                db.Add(rate);
                db.SaveChanges();
            }
        }

        //Bætir við athugasemd við eftirfarandi bók hafi notandinn skrifað eitthvað.
        public void AddComment(string userName, string text, int book)
        {
            if(!string.IsNullOrEmpty(text))
            {
                var comment = new Comment(){UserName = userName, BookId = book, Text = text};
                db.Add(comment);
                db.SaveChanges();
            }
        }

        //Uppfærir meðaleinkunn bókarinnar eftir að nýrri einkunn hefur verið bætt við
        public void UpdateRating(int ratingId, float newRating) 
        {
            var rate = db.Ratings.FirstOrDefault(r => r.Id == ratingId);
            rate.Rate = newRating;
            db.Update(rate);
            db.SaveChanges();
        }

        //Skilar öllum bókum sem til eru í gagnagrunninum
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

            //12 nýjustu bækurnar í gagnagrunninum útfrá útgáfudegi
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

            //12 einkunnarhæstu bækurnar
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
                            Name = a.Name,
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
                            select new CommentViewModel
                            {
                                Id = c.Id,
                                Text = c.Text,
                                UserName = c.UserName
                            }).ToList();
            
            var book = (from b in db.Books
                        join g in db.Genre on b.GenreId equals g.Id
                        join a in db.Authors on b.AuthorId equals a.Id
                        where b.Id == Id
                        select new BookDetailsViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Price = b.Price, 
                            Author = a.Name, 
                            Image = b.Image,
                            Genre = g.TheGenre,
                            Comments = comments,
                            Description = b.Description,
                            Rating = rating.Average()
                        }).SingleOrDefault(); 
            return book;
        }

        public List<BookListViewModel> OrderBooks(string order, int genreId)
        {
            var books = (from b in db.Books
                        select b);
            
            if(genreId != 0)
            {
                books = (from b in db.Books
                            join g in db.Genre on genreId equals g.Id
                            where genreId == b.GenreId
                            select b);
            }

            if(order == "highest")
            {
                 var correctBooks = (from b in books
                                orderby b.Price descending
                                select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                    }).ToList();
                return correctBooks;
            }
            
            else if( order == "lowest")
            {
                var correctBooks = (from b in books
                        orderby b.Price ascending
                        select new BookListViewModel
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Image = b.Image,
                            Price = b.Price,
                        }).ToList();
                return correctBooks;
            }
            else
            {
                var correctBooks = (from b in books
                                    orderby b.Name
                                    select new BookListViewModel
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Image = b.Image,
                                        Price = b.Price,
                                    }).ToList();
                return correctBooks;
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

        /*public List<BookListViewModel> FilterBooks(string filterChoice)
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

        }*/
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