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
            if(user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User updateUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NumberDocument == updateUser.NumberDocument);

            if(user == null)
            {
                return NotFound("User not found");
            }

            user.Address = updateUser.Address;
            user.PhoneNumber = updateUser.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok("User updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("User deleted successfully");
        }
    }
}