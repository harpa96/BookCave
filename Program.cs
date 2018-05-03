using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void SeedData()
        {
            var db = new DataContext();

            if(!db.Books.Any())
            {
                var initialBooks = new List<Book>()
                {
                    new Book{Name = "10 ráð til að hætta að drepa fólk og byrja að vaska upp"},
                    new Book{Name = "Týnda systirin"},
                    new Book{Name = "Gatið"},
                    new Book{Name = "Blóðengill"},
                    new Book{Name = "Átta gata Buick"},
                    new Book{Name = "El Príncipe de la Niebla"}
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
