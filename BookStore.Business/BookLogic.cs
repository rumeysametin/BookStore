using BookStore.Data.Models;
using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Services;

namespace BookStore.Business
{
    public class BookLogic
    {
        private readonly BookService _bookService;

        public BookLogic(BookStoreContext context)
        {
            _bookService = new BookService(context);
        }

        public async Task<List<Book>> GetAllBooks()
        { 
            return await _bookService.GetAllBooks();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookService.GetBookById(id);
        }

        public async Task AddBook(Book book)
        {
            await _bookService.AddBook(book);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookService.UpdateBook(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
        }
    }
}
