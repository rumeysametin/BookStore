
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Business;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookLogic _bookLogic;

        public BooksController(BookLogic bookLogic)
        {
            _bookLogic = bookLogic;
        }   

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookLogic.GetAllBooks();
            return Ok(books);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookLogic.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //    await _bookLogic.AddBook(book);
        //    return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        //}
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book object is null");
            }
            await _bookLogic.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
        {
            if (id != book.BookID)
            {
                return BadRequest();
            }

            await _bookLogic.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookLogic.DeleteBook(id);
            return NoContent();
        }
    }
}
