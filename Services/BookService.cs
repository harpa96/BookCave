using System.Collections.Generic;
using BookCave.Models.ViewModels;


namespace BookCave.Services
{
    public class BookService
    {
        public List<BookListViewModel> GetAllBooks()
        {
            var books = new List<BookListViewModel>
            {
                new BookListViewModel { Id=1, Name="Lord Of The Rings"},
                new BookListViewModel { Id=1, Name="Gisla saga surssonar"},
                new BookListViewModel { Id=1, Name="Jolasnjor"},
                new BookListViewModel { Id=1, Name="Siggi og Gisli"},
            };
            return books;
        }

    }
}