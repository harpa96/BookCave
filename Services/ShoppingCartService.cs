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
        
        public List<BookCartViewModel> GetCart(string userId)
        {
            var cart = (from c in db.Cart
                        join b in db.Books on c.BookId equals b.Id
                        join a in db.Authors on b.AuthorId equals a.Id
                        where userId == c.UserId
                        select new BookCartViewModel
                        {
                            Id = b.Id,
                            Name = b.Name, 
                            Price = b.Price,
                            PriceSum = b.Price*c.Copies,
                            Copies = c.Copies,
                            Image = b.Image,
                            Author = a.Name
                        }).ToList();

            return cart;
        }
        
        public int GetTotal(string userId)
        {
            var cart = GetCart(userId);
            var totalPrice = 0;

            foreach (var cartItem in cart)
            {
                totalPrice += cartItem.PriceSum;
            }
            
            return totalPrice;
        }
        
        public void AddToCart(BookDetailsViewModel book, string userId)
        {
            var searchBook = (from c in db.Cart
                    where book.Id == c.BookId && userId == c.UserId
                    select c).SingleOrDefault();
            
            //Ef bókin er ekki nú þegar í körfunni
            if (searchBook == null)
            {
                var newBook = new Cart
                {
                    UserId = userId,
                    BookId = book.Id,
                    Copies = book.Copies
                };

                db.Add(newBook);
            }

            //Ef bókin er í körfunni þá bætum við bara við fjölda copies hér frekar en að bæta við nýrri færslu
            else
            {
                searchBook.Copies += book.Copies;
            }

            //Uppfærum gagnagrunninn
            db.SaveChanges();
        }

        public void ClearCart(string userId)
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
        
            //Eyðum einni bók út í einu í staðinn fyrir að eyða þeim öllum.
            if(theBook.Copies > 1)
            {
                theBook.Copies--;
                db.Update(theBook);
            }

            //Ef eintakið sem við erum að eyða er eina eintakið sem er eftir 
            else
            {
                db.Cart.Remove(theBook);
            }
            
            db.SaveChanges();
        }

    }
}