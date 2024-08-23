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


        // GET: /User/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var user = _context.Users.ToList();
            return View(user);
        }

        // POST: /User/Index
        [HttpPost]
        public IActionResult Index(string correo, string clave)
        {
            // Lógica de autenticación
            if (correo == "Konoe@gmail.com" && clave == "123")
            {
                // Redirige a otra acción o vista
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas.";
                return View();
            }
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
        public async Task<IActionResult> UpdateUser(int id)
        {
            User user = await _context.Users.FirstAsync(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<User> list = await _context.Users.ToListAsync();
            return View(list);
        }
    }
}