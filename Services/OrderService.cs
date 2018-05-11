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
        private ShoppingCartService _shoppingCart;

        public OrderService()
        {
            db = new DataContext();
            _shoppingCart = new ShoppingCartService();
        }

        public void addNewOrder(string userId)
        {
            var cart = (from c in db.Cart
                           where userId == c.UserId
                           select c).ToList();
            
            if(cart.Count > 0)
            {
                var newOrder = new Order{UserId = userId};
                db.Add(newOrder);
                db.SaveChanges();

                foreach(var book in cart)
                {
                    var bookInOrder = new OrderedBooks
                    {
                        OrderId = newOrder.Id,
                        BookId = book.BookId,
                        Copies = book.Copies
                    };

                    db.Add(bookInOrder);
                }
                db.SaveChanges();
            }

            
        }

        public string GetCountry(int cId)
        {
            var country = (from c in db.Countries 
                            where cId == c.Id
                            select c.Name).FirstOrDefault().ToString();

            return country;
        }
        
        public int getOrderTotalPrice( int orderId)
        {
            var books = (from bo in db.BooksInOrder
                        join b in db.Books on bo.BookId equals b.Id
                        where bo.OrderId == orderId
                        select new BookCartViewModel
                        {
                            Price = b.Price,
                            Copies = bo.Copies
                        }).ToList();

            var sum = 0;
            foreach(var book in books)
            {
                sum += book.Copies*book.Price;
            } 

            return sum;
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
                    Id = orderId,
                    Books = (from ob in db.BooksInOrder
                                join b in db.Books on ob.BookId equals b.Id
                                where ob.OrderId == orderId
                                select new BookCartViewModel
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Image = b.Image,
                                    Price = b.Price,
                                    Copies = ob.Copies,
                                    PriceSum = b.Price*ob.Copies

                                }).ToList(),
                    TotalPrice = getOrderTotalPrice(orderId)
                    
                };
                OrderList.Orders.Add(order);
            }
            return OrderList;
        }
    }
}