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


         modelBuilder.Entity<User>(tb => {
                tb.HasKey(col => col.Id);

                tb.Property(col => col.Id).UseMySqlIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Name).HasMaxLength(50);
                tb.Property(col => col.LastName).HasMaxLength(50);
                tb.Property(col => col.Address).HasMaxLength(100);
                tb.Property(col => col.PhoneNumber).HasMaxLength(50);
                tb.Property(col => col.NumberDocument).HasMaxLength(50);
                tb.Property(col => col.Role).HasMaxLength(50);
                tb.Property(col => col.TypeDocument).HasMaxLength(50);

                //tb.ToTable("Jugadores"); //Cambiamos el nombre de la tabla a Jugadores en lugar de Jugador

            });

        modelBuilder.Entity<User>().ToTable("Users");




         modelBuilder.Entity<Book>(tb => {
                tb.HasKey(col => col.Id);

                tb.Property(col => col.Id).UseMySqlIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Title).HasMaxLength(50);
                tb.Property(col => col.Category).HasMaxLength(50);
                tb.Property(col => col.Author).HasMaxLength(100);
                tb.Property(col => col.ISBN).HasMaxLength(50);
                tb.Property(col => col.State).HasMaxLength(50);

                //tb.ToTable("Jugadores"); //Cambiamos el nombre de la tabla a Jugadores en lugar de Jugador

            });
            
        modelBuilder.Entity<Book>().ToTable("Books");
    }

}
