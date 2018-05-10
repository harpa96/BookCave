using System;
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
           
        }

        
    }
}
