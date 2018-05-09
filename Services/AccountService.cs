using BookCave.Data;

namespace BookCave.Services
{
    public class AccountService
    {
        private DataContext db;

        public AccountService()
        {
            db = new DataContext();
        }

        
    }
}