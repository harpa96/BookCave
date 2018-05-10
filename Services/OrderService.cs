using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;


namespace BookCave.Services
{
    public class OrderService
    {
        private DataContext db;

        public OrderService()
        {
            db = new DataContext();
        }
        
        public void AddOrder(int order, int user, int book, int copies)
        {
            var neworder = new Order(){ Id = order, UserId = user };
            var newBook = new OrderedBooks(){OrderId=order, BookId = book, Copies = copies};
            db.Add(neworder);
            db.Add(newBook);
            db.SaveChanges();
        }

        public void UpdateOrder(int userId, int newcopies, int bookId) 
        {
            var order = db.Orders.FirstOrDefault(o => o.UserId == userId);
            var book = (from b in db.BooksInOrder
                        join o in db.Orders on b.OrderId equals o.Id
                        where o.UserId == userId && b.Id == bookId
                        select b).FirstOrDefault();
                        
            book.Copies = newcopies;
            db.Update(book);
            db.SaveChanges();
        }

        public List<OrderListViewModel> GetOrderByUser(int? Id)
        {
            var order = (from b in db.BooksInOrder
                        join o in db.Orders on b.OrderId equals o.Id
                        join bo in db.Books on b.BookId  equals bo.Id
                        join a in db.Authors on bo.AuthorId equals a.Id
                        where o.UserId == Id
                        select new OrderListViewModel
                        { 
                            BookName = bo.Name,
                            Image = bo.Image,
                            Price = bo.Price,
                            AutorName = a.Name,
                            Copies = b.Copies,
                        }).ToList();
            return order;
        }
    }
  

}