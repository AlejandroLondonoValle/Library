using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataBase;

public class AplicationDbContext : DbContext
{
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Loan>()
            .HasKey(l => l.IdPrestamo);

            
        // Relación entre Loan y User (uno a muchos)
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.IdUser)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.IdUserId);

        // Relación entre Loan y Book (uno a muchos)
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookISBN)
            .HasPrincipalKey(b => b.ISBN);

        // Configuración adicional según sea necesario


        modelBuilder.Entity<User>().ToTable("User");
    }

}
