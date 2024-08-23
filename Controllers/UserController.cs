using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DataBase;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class UserController : Controller
    {

        private readonly AplicationDbContext _context;

        public UserController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var books = _context.Books.ToList();
            return View(books);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByDocument(string document)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NumberDocument == document);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Book> list = await _context.Books.ToListAsync();
            return View(list);
        }

    }
}