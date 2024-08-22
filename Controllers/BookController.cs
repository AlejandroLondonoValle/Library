using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DataBase;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("books")] // Ruta base para todas las acciones
    public class BookController : Controller
    {
        private readonly AplicationDbContext _context;

        public BookController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok("Libro creado correctamente");
        }

        [HttpGet("get/{title}")]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
            if (book == null)
            {
                return NotFound("Libro no encontrado.");
            }
            return Ok(book);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updateBook)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == updateBook.Title);
            if (book == null)
            {
                return NotFound("Libro no encontrado.");
            }
            book.Author = updateBook.Author;
            book.Category = updateBook.Category;
            book.Title = updateBook.Title;
            book.State = updateBook.State;
            book.ISBN = updateBook.ISBN;

            await _context.SaveChangesAsync();
            return Ok("Libro actualizado correctamente.");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Read));
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            List<Book> list = await _context.Books.ToListAsync();
            return View(list);
        }
    }
}