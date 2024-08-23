using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.DataBase;

namespace Library.Controllers
{
 
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

   
    }
}