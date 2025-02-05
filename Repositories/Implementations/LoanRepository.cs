using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;

public class LoanRepository : GenericRepository<Loan>, ILoanRepository
{
    public List<Loan> GetAllWithBorrowerAndBooks()
        => _context.Loans.Where(l => l.ReturnDate == null)
                         .Include(l => l.Borrower)
                         .Include(l => l.LoanItems)
                           .ThenInclude(li => li.Book)
                         .ToList();

    public Loan? GetByIdWithBorrowerAndBooks(int id)
        => _context.Loans.Where(l => l.ReturnDate == null)
                         .Include(l => l.Borrower)
                         .Include(l => l.LoanItems)
                           .ThenInclude(li => li.Book)
                         .FirstOrDefault(l => l.Id == id);
}
