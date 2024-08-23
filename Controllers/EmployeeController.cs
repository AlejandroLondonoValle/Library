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


public class EmployeeController : Controller
{
    private readonly AplicationDbContext _context;

    public EmployeeController(AplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Employee()
    {
        return View();
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
        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateBook(int id)
    {
        Book book = await _context.Books.FirstAsync(b => b.Id == id);
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBook(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ListBook));
    }


    [HttpGet]
    public async Task<IActionResult> ListBook()
    {
        List<Book> list = await _context.Books.ToListAsync();
        return View(list);
    }
 [HttpGet]
    public async Task<IActionResult> List()
    {
        List<User> list = await _context.Users.ToListAsync();
        return View(list);
    }

        [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return Ok("Prestamo generado correctamente");

    }
    [HttpGet]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var loan = await _context.Loans.FirstOrDefaultAsync(l => l.IdPrestamo == id);
        if (loan == null)
        {
            return NotFound("Prestamo no encontrado");
        }

        return Ok(loan);
    }

    [HttpPut]

    public async Task<IActionResult> UpdateLoan([FromBody] Loan updateLoan)
    {
        var loan = await _context.Loans.FirstOrDefaultAsync(l => l.IdPrestamo == updateLoan.IdPrestamo);
        if (loan == null)
        {
            return NotFound("Prestamo no encontrado");

        }

        loan.State = updateLoan.State;
        loan.BookISBN = updateLoan.BookISBN;
        loan.IdUser = updateLoan.IdUser;
        loan.StartDate = updateLoan.StartDate;
        loan.FinishDate = updateLoan.FinishDate;


        await _context.SaveChangesAsync();
        return Ok("Prestamo Actualizado correctamente");
    }


}





