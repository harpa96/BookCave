using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using System.Net.Mail;
using System.Net;
using System;

namespace BookCave.Services
{
    public class OrderService : IOrderService
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
        public void ProcessOrder(CheckoutViewModel order)
        {
            if(string.IsNullOrEmpty(order.ReceiverName)) {throw new Exception("Name is missing");}
            if(string.IsNullOrEmpty(order.ReceiverPhoneNumber)) {throw new Exception("Phone is missing");}
            if(string.IsNullOrEmpty(order.ReceiverAddress)) {throw new Exception("Address is missing");}
            if(string.IsNullOrEmpty(order.ReceiverCity)) {throw new Exception("City is missing");}
            if(string.IsNullOrEmpty(order.ReceiverZIP)) {throw new Exception("Zip is missing");}
            if(string.IsNullOrEmpty(order.PayerName)) {throw new Exception("Payer Name is missing");}
            if(string.IsNullOrEmpty(order.PayerPhoneNumber)) {throw new Exception("Payer Phone is missing");}
            if(string.IsNullOrEmpty(order.PayerAddress)) {throw new Exception("Payer Address is missing");}
            if(string.IsNullOrEmpty(order.PayerCity)) {throw new Exception("Payer city is missing");}
            if(string.IsNullOrEmpty(order.PayerZIP)) {throw new Exception("Payer zip is missing");}
            if(string.IsNullOrEmpty(order.PayerEmail)) {throw new Exception("Payer email is missing");}
        }
        public void SendOrderEmail(CheckoutViewModel order)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("contactus.bookcave@gmail.com", "BookCave1");
            client.Port = 587;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contactus.bookcave@gmail.com");
            mailMessage.To.Add(order.PayerEmail);
            mailMessage.Body = "<p>Kæri/Kæra <b>" + order.PayerName + "</b>, greiðslan þín hefur farið í gegn. Þú munt fá staðfestingu á næstu dögum þegar pöntun þín er tilbúin til afhendingar.</p>";
            mailMessage.Subject = "Greiðslustaðfesting";
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
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