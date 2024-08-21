using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models;

public class User
{
    public int Id { get; set; }
    public string TypeDocument { get; set; }
    public string NumberDocument { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    

}
