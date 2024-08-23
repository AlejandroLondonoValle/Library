using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.DataBase;
using Library.Models;
using Microsoft.EntityFrameworkCore;
namespace Library.Controllers;


public class LoginController : Controller
{
    private readonly AplicationDbContext _context;

    public LoginController(AplicationDbContext context)
    {
        _context = context;
    }


    // GET: /Login/Index
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

    // POST: /Login/Index
    [HttpPost]
    public async Task<IActionResult> Index(string name, string numberDocument)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Name == name && u.NumberDocument == numberDocument);

        // Lógica de autenticación
        if (user != null)
        {
            // Redirige a otra acción o vista si la autenticación es exitosa
            return RedirectToAction("Dashboard");
        }
        else
        {
            ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }
    }

    // GET: /Login/Error
    public IActionResult Error()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        User user = await _context.Users.FirstAsync(u => u.Id == id);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
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
