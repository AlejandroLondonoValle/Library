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

    [Route("[controller]")]
    public class LoanController : Controller
    {
        private readonly AplicationDbContext _context;

        public LoanController(AplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public async Task<IActionResult>CreateLoan([FromBody]Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return Ok("Prestamo generado correctamente");

        }
        [HttpGet]
        public async Task<IActionResult> GetLoanById (int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l=>l.IdPrestamo==id);
            if (loan == null)
            {
                return NotFound("Prestamo no encontrado");
            }

            return Ok(loan);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateLoan ([FromBody]Loan updateLoan)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l=>l.IdPrestamo== updateLoan.IdPrestamo);
            if ( loan ==null)
            {return NotFound("Prestamo no encontrado");
                
            }

            loan.State = updateLoan.State;
            loan.BookISBN=updateLoan.BookISBN;
            loan.IdUser= updateLoan.IdUser;
            loan.StartDate=updateLoan.StartDate;
            loan.FinishDate=updateLoan.FinishDate;




            await _context.SaveChangesAsync();
            return Ok("Prestamo Actualizado correctamente");
        }





    }

