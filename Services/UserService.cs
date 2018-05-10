/*using System;
using BookCave.Models;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public class UserService : IUserService
    {
        public void ProcessRegister(RegisterViewModel register)
        {
            if(string.IsNullOrEmpty(register.Email)) {throw new Exception("Email is missing");}
            if(string.IsNullOrEmpty(register.FirstName)) {throw new Exception("First name is missing");}
            if(string.IsNullOrEmpty(register.LastName)) {throw new Exception("Last name is missing");}
            if(string.IsNullOrEmpty(register.Password)) {throw new Exception("Password is missing");}
            if(string.IsNullOrEmpty(register.Address)) {throw new Exception("Address is missing");}
            if(string.IsNullOrEmpty(register.City)) {throw new Exception("City is missing");}
            if(string.IsNullOrEmpty(register.ZIP)) {throw new Exception("Zip is missing");}
        }

        
    }
}*/