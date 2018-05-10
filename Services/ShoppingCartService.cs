using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookCave.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookCave.Services
{
    public class ShoppingCartService
    {
        private DataContext db;

        public ShoppingCartService()
        {
            db = new DataContext();
           
        }
        
        [Authorize]
        public List<BookCartViewModel> getCart(string userId)
        {
            var cart = (from c in db.Cart
                        join b in db.Books on c.BookId equals b.Id
                        where userId == c.UserId
                        select new BookCartViewModel
                        {
                            Id = b.Id,
                            Name = b.Name, 
                            Price = b.Price,
                            PriceSum = b.Price*c.Copies,
                            Copies = c.Copies,
                            Image = b.Image
                        }).ToList();

            return cart;
        }

        public void addToCart(BookDetailsViewModel book, string userId)
        {
            var searchBook = (from c in db.Cart
                    where book.Id == c.BookId && userId == c.UserId
                    select c).SingleOrDefault();
            
            if(searchBook == null)
            {
                var newBook = new Cart
                {
                    UserId = userId,
                    BookId = book.Id,
                    Copies = book.Copies
                };
                db.Add(newBook);
            }
            else
            {
                searchBook.Copies += book.Copies;
            }

            db.SaveChanges();
        }

        public void clearCart(string userId)
        {
                var allBooks = (from c in db.Cart
                    where c.UserId == userId
                    select c);

                foreach (var b in allBooks)
                {
                    db.Remove(b);
                }
                db.SaveChanges();
        }

        public void removeFromCart(BookDetailsViewModel book, string userId)
        {
                var theBook = (from c in db.Cart
                where userId == c.UserId && book.Id == c.BookId
                select c).FirstOrDefault();
            
                if(theBook.Copies > 1)
                {
                    theBook.Copies--;
                    db.Update(theBook);
                }

                else
                {
                    db.Cart.Remove(theBook);
                }
            
            db.SaveChanges();
        }

    }
}