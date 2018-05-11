using System.Collections.Generic;
using BookCave.Data;
using System.Linq;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using System.Net.Mail;
using System.Net;

namespace BookCave.Services
{
    public class OrderService : IOrderService
    {
        private DataContext db;

        public OrderService()
        {
            db = new DataContext();
        }
       /* 
        public void ProcessOrder(CheckoutViewModel order)
        {
            if(string.IsNullOrEmpty(order.Name)) {throw new Exception("Name is missing");}
            if(string.IsNullOrEmpty(order.Email)) {throw new Exception("Email is missing");}
            if(string.IsNullOrEmpty(order.Amount)) {throw new Exception("Amount is missing");}
            if(!donate.Checked){throw new Exception("Checked is missing");}
        }
*/
        public void SendOrderEmail(CheckoutViewModel order)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("contactus.bookcave@gmail.com", "BookCave1");
            client.Port = 587;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contactus.bookcave@gmail.com");
            mailMessage.To.Add(order.PayerName);
            mailMessage.Body = "<p>Kæri/Kæra <b>" + order.PayerName + "</b>, greiðslan þín hefur farið í gegn.</p>";
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
    }
}