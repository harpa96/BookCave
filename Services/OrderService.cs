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
        
        /*public void AddOrder(int order, int user, int book, int copies)
        {
            var neworder = new Order(){ Id = order, UserId = user. };
            var newBook = new OrderedBooks(){OrderId=order, BookId = book, Copies = copies};
            db.Add(neworder);
            db.Add(newBook);
            db.SaveChanges();
        }*/

        public void UpdateOrder(string userId, int newcopies, int bookId) 
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

        public OrderListViewModel GetOrdersForUser(string userId)
        {
            var OrderList = new OrderListViewModel
            {
                userId = userId
            };

            OrderList.Orders = new List<OrderViewModel>();
            
            var idOrders = (from o in db.Orders
                          where userId == o.UserId
                          select o.Id).ToList();
            
            foreach(var orderId in idOrders)
            {
                var order = new OrderViewModel
                {
                    Books = (from ob in db.BooksInOrder
                                join b in db.Books on ob.BookId equals b.Id
                                where ob.OrderId == orderId
                                select new BookCartViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Price = b.Price,
                                    Copies = ob.Copies

                                }).ToList()
                };
                OrderList.Orders.Add(order);
            }
            return OrderList;;
        }

        /*public List<OrderListViewModel> GetOrderByUser(string userId)
        {
            var order = (from b in db.BooksInOrder
                        join o in db.Orders on b.OrderId equals o.Id
                        join bo in db.Books on b.BookId  equals bo.Id
                        join a in db.Authors on bo.AuthorId equals a.Id
                        where o.UserId == userId
                        select new OrderListViewModel
                        { 
                            BookName = bo.Name,
                            Image = bo.Image,
                            Price = bo.Price,
                            AutorName = a.Name,
                            Copies = b.Copies,
                        }).ToList();
            return order;
        }*/
    }
  

}