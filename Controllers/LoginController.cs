using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Library.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/Index
        [HttpPost]
        public IActionResult Index(string correo, string clave)
        {
            // Lógica de autenticación
            if (correo == "Konoe@gmail.com" && clave == "123")
            {
                // Redirige a otra acción o vista
                return RedirectToAction("Success");
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
    }
}