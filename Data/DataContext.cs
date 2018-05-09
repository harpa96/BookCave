using BookCave.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BookCave.Data
{
     public class DataContext : DbContext
     {
        public DbSet<Book> Books {get; set; }
        public DbSet<Author> Authors {get; set;}
        public DbSet<Rating> Ratings {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<OrderedBooks> BooksInOrder {get; set;}
        public DbSet<Genre> Genre {get; set;}        
        public DbSet<Country> Countries { get; set;}

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder 
                .UseSqlServer(
                    "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H23;Persist Security Info=False;User ID=VLN2_2018_H23_usr;Password=dampFerre+21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                ); 
         }
     }
}