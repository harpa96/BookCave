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
    
        public BookService()
        {
            db = new DataContext();
        }

        //bætir við einkunn við eftirfarandi bók hafi notandi
        //Rating færibreytan getur verið null ef notandi bætti bara við kommenti ekki einkunn
        public void AddRating(float? rating, int book)
        {
            //Myndi vera null ef notandi fyllti ekki inn í gildi fyrir Rating 
            if (rating != null)
            {
                var rate = new Rating()
                { 
                    BookId = book, 
                    Rate = (float)rating
                };

                //Bæta við upplýsingunum í gagnagrunninn
                db.Add(rate);
                db.SaveChanges();
            }
        }

        //Bætir við athugasemd við eftirfarandi bók en bara ef notandinn skrifaði eitthvað.
        public void AddComment(string userName, string text, int book)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var comment = new Comment()
                {
                    UserName = userName, 
                    BookId = book, 
                    Text = text
                };

                //Bæta við upplýsingunum í gagnagrunninn
                db.Add(comment);
                db.SaveChanges();
            }
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

        //Finnur 10 einkunnahæstu bækurnar í gagnagrunninum
        public List<BookListViewModel> GetTop()
        { 
            //Búum til þennan "temp" lista  til að geta raðað lokalistanum eftir meðaltali á gefnum einkunum
            var topBooks = (from b in db.Books
                            join g in db.Genre on b.GenreId equals g.Id
                            select new BookListViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                Price = b.Price,
                                Image = b.Image,
                                Rating = (from r in db.Ratings 
                                            where r.BookId == b.Id 
                                            select r.Rate).ToList()
                            }).ToList();
                        
            var returnValue = (from b in topBooks
                                orderby b.Rating.DefaultIfEmpty(0).Average() descending
                                select b).Take(10).ToList();
            
            return returnValue;
        }
        public List<BookListViewModel> FilterCategories(int? id)
        {
            //Ef reynt er að fara í category viewið án þess að vera með route-id
            if (id == null)
            {
                return GetAllBooks();
            }
            
            //Þær bækur sem hafa það Genre sem beðið var um
            var allBooks = (from b in db.Books
                            join g in db.Genre on b.GenreId equals g.Id
                            where b.GenreId == id
                            select new BookListViewModel
                            {
                                Id = b.Id,
                                Name = b.Name,
                                Price = b.Price,
                                Image = b.Image
                            }).ToList();
            
            return allBooks;
        }

        public BookDetailsViewModel FindBookById (int? id)
        {
            var checkId = (from b in db.Books
                            where id == b.Id
                            select b).SingleOrDefault();
            
            //Sjá hvort það sé bók í gagnagrunninum með þetta tiltekna id
            if (checkId == null)
            {
                return null;
            }

            var rating = (from r in db.Ratings
                        where r.BookId == id
                        select r.Rate).ToList();

            var comments = (from c in db.Comments
                            where c.BookId == id
                            select new CommentViewModel
                            {
                                Id = c.Id,
                                Text = c.Text,
                                UserName = c.UserName
                            }).ToList();
            
            var book = (from b in db.Books
                        join g in db.Genre on b.GenreId equals g.Id
                        join a in db.Authors on b.AuthorId equals a.Id
                        where b.Id == id
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
            
            if (genreId != 0)
            {
                books = (from b in db.Books
                        join g in db.Genre on genreId equals g.Id
                        where genreId == b.GenreId
                        select b);
            }

            //Raðað eftir gildi sem valið var úr dropdowni í category view
            //Hér eftir hæsta verði
            if (order == "highest")
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
            
            //Eftir lægsta verði
            else if (order == "lowest")
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

            //Ef notandi vildi raða eftir nafni bókar
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
            //Skilar lista af þeim bókum sem innihalda searchInput annað hvort í nafni bókar eða höfundar
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
                                    }).ToList();
                
                return searchedBooks;
            }

            //ef það var enginn strengur til að bera saman við
            var books = (from b in db.Books
                        join g in db.Genre on b.GenreId equals g.Id
                        select new BookListViewModel
                        {
                            Id = b.Id, 
                            Name = b.Name,
                            Image = b.Image,
                            Price = b.Price
                        }).ToList();
            
            return books;
        }

        public string GetGenre(int? id)
        {
            string genre = (from g in db.Genre
                            where id == g.Id
                            select g.TheGenre).SingleOrDefault();
            
            return genre;
        }

        public string GetNameOfBook(int id)
        {
            var name = (from b in db.Books
                        where id == b.Id
                        select b.Name).ToString();

            return name;
        }

    }
}