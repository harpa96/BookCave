using System;
using BookCave.Models;
using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public class DonateService : IDonateService
    {
        public void ProcessDonate(DonateInputModel donate)
        {
            if(string.IsNullOrEmpty(donate.Name)) {throw new Exception("Name is missing");}
            if(string.IsNullOrEmpty(donate.Email)) {throw new Exception("Email is missing");}
            if(string.IsNullOrEmpty(donate.Amount)) {throw new Exception("Amount is missing");}
            if(!donate.Checked.Equals("yes"))
            {
              throw new Exception("Checked is missing");
            }
        }
    }
}