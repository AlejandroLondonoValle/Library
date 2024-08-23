using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Library.DataBase;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Controllers;

public class LoanController : Controller
{
    private readonly AplicationDbContext _context;

    public LoanController(AplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Loan> Index = await _context.Loans.ToListAsync();
        return View(Index);
    }

    [HttpGet]
    public IActionResult NewLoan()
    {
        var loan = new Loan(); // Crear una instancia de Loan
        return View(loan); // Pasar la instancia a la vista
    }


    [HttpPost]
    public async Task<IActionResult> NewLoan(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}

