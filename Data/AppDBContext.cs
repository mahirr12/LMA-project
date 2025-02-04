using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entitys;

namespace Project___ConsoleApp__Library_Management_Application_.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-U7KTMUF4\\SQLEXPRESS;Database=LMA_project;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
