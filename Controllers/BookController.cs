using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Library.DataBase;
using Library.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Controllers;

    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly AplicationDbContext _context;

        public BookController(AplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

                [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody]Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok("Usuario creado completamente");
        }


[HttpGet]
        public async Task<IActionResult> GetBookByDocument(string title)
        {
            var book = await _context.Books.FirstOrDefaultAsync (b=>b.Title== title);
            if (book==null)
            {
                return NotFound ("Libro no encontrado ");
            };

            return Ok (book);

            
        }
    [HttpPut]
        public async Task<IActionResult> UpdateBook ([FromBody]Book updateBook)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b=> b.Title == updateBook.Title);
            if (book==null)
            {
                return NotFound("Libro no encontrado.");

            }
            book.Author=updateBook.Author;
            book.Category=updateBook.Category;
            book.Title=updateBook.Title;
            book.State=updateBook.State;
            book.ISBN=updateBook.ISBN;
            

            await _context.SaveChangesAsync();
            return Ok ("Libro Actualizado correctamente");
        }
        [HttpGet]
        public async Task<IActionResult>DeleteBook(string isbn)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b=> b.ISBN== isbn);
            if (book == null)
            {
                return NotFound("Libro no encontrado");
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok("Libro eliminado correctamente");
        }

    }
