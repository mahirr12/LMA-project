using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;

public class BorrowerRepository : GenericRepository<Borrower>, IBorrowerRepository
{
    public List<Borrower> GetAllWithLoans()
        => _context.Borrowers.Where(b => !b.IsDeleted)
                             .Include(b => b.Loans)
                                .ThenInclude(l => l.LoanItems)
                                   .ThenInclude(li => li.Book)
                             .ToList();

    public Borrower? GetByIdWithLoans(int id)
        => _context.Borrowers.Where(b => !b.IsDeleted)
                             .Include(b => b.Loans)
                                .ThenInclude(l => l.LoanItems)
                                   .ThenInclude(li => li.Book)
                             .FirstOrDefault(b => b.Id == id);


}
