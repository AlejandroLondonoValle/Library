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
        public Book ISBN { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool State { get; set; }
    }
}