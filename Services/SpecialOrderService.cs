using System;
using System.Net;
using System.Net.Mail;
using BookCave.Models;
using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public class SpecialOrderService : ISpecialOrderService
    {
        public void ProcessSpecialOrder(SpecialOrderInputModel specialorder)
        {
            if(string.IsNullOrEmpty(specialorder.Name)) {throw new Exception("Name is missing");}
            if(string.IsNullOrEmpty(specialorder.Phone)) {throw new Exception("Phone is missing");}
            if(string.IsNullOrEmpty(specialorder.Address)) {throw new Exception("Address is missing");}
            if(string.IsNullOrEmpty(specialorder.Zip)) {throw new Exception("Zip is missing");}
            if(string.IsNullOrEmpty(specialorder.City)) {throw new Exception("City is missing");}
            if(string.IsNullOrEmpty(specialorder.Email)) {throw new Exception("Email is missing");}
            if(string.IsNullOrEmpty(specialorder.Title)) {throw new Exception("Title is missing");}
            if(string.IsNullOrEmpty(specialorder.Author)) {throw new Exception("Author is missing");}
            if(string.IsNullOrEmpty(specialorder.PublishDate)) {throw new Exception("PublishDate is missing");}
            if(specialorder.Amount < 20) {throw new Exception("Amount is missing");}
        }

        public void SendSpecialOrderEmail(SpecialOrderInputModel specialorder)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("contactus.bookcave@gmail.com", "BookCave1");
            client.Port = 587;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contactus.bookcave@gmail.com");
            mailMessage.To.Add("contactus.bookcave@gmail.com");
            mailMessage.Body = "<p>Ný sérpöntun frá <b>" + specialorder.Name + "</b>, hún/hann er með netfangið: <b>" + specialorder.Email + "</b> . <h3>Upplýsingar um viðskiptavin</h3> </hr> <table><tr><th>Nafn</th><th>Símanúmer</th><th>Heimilisfang</th><th>Póstnúmer</th><th>Borg</th></tr><tr><td>" + specialorder.Name + "</td><td>" + specialorder.Phone + "</td><td>" + specialorder.Address + "</td><td>" + specialorder.Zip + "</td><td>" + specialorder.City + "</td></tr> </table> </br></br></br> <h3>Pöntun hennar/hans er eftirfarandi:</h3> </hr> </br></br> <table><tr><th>Titill</th><th>Höfundur</th><th>Útgáfuár</th><th>Magn</th></tr><tr><td>" + specialorder.Title + "</td><td>" + specialorder.Author + "</td><td>" + specialorder.PublishDate + "</td><td>" + specialorder.Amount + "</td></tr> </table></p>";
            mailMessage.Subject = "Sérpöntun";
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        } 

        
    }
}
