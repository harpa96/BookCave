using System;
using BookCave.Models;
using BookCave.Models.InputModels;
using System.Net.Mail;
using System.Net;

namespace BookCave.Services
{
    public class DonateService : IDonateService
    {
        public void ProcessDonate(DonateInputModel donate)
        {
            if(string.IsNullOrEmpty(donate.Name)) {throw new Exception("Name is missing");}
            if(string.IsNullOrEmpty(donate.Email)) {throw new Exception("Email is missing");}
            if(string.IsNullOrEmpty(donate.Amount)) {throw new Exception("Amount is missing");}
            if(donate.Checked == false){throw new Exception("Amount is missing");}
        }

        public void SendDonateEmail(DonateInputModel donate)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("contactus.bookcave@gmail.com", "BookCave1");
            client.Port = 587;
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contactus.bookcave@gmail.com");
            mailMessage.To.Add(donate.Email);
            mailMessage.Body = "<p>Sæl/Sæll <b>" + donate.Name + "</b>, Mikið ert þú með gott hjarta. Nú munu fleiri börn í Afríku hafa tækifæri á að læra að lesa. Greiðslan þín: <b>" + donate.Amount + " </b>kr. hefur farið í gegn, kærar þakkir fyrir stuðninginn.</p>";
            mailMessage.Subject = "Greiðslustaðfesting";
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        } 
    }
}