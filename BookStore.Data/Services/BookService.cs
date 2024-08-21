using BookStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookStore.Data.Services
{
    public class BookService
    {
        private readonly BookStoreContext _context; //veritabanı ile iletişim 

        public BookService(BookStoreContext context) //veritabanını sınıfa çevirir
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks() //tüm kitapları alır ve liste olarak döndürür
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id) //isteneilen id ye göre kitabı bulur
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddBook(Book book) //yeni kitap ekleme
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book) //Güncelleme
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id) //Silme
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task GetBookByTitle(string bookTitle)
        {
            throw new NotImplementedException();
        }
    }
}
