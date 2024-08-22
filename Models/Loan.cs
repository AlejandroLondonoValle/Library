using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Loan
    {
        public int IdPrestamo { get; set; }
        public User IdUser { get; set; }
        public int IdUserId { get; set; } // Propiedad de clave foránea
        public Book Book { get; set; }
        public string BookISBN { get; set; } // Propiedad de clave foránea
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool State { get; set; }
    }
}