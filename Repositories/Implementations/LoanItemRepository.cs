using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;

public class LoanItemRepository : GenericRepository<LoanItem>, ILoanItemRepository
{
    public (int? bookId, int count) MostBorrowedBook()
    {
        var result = _context.LoanItems.GroupBy(li => li.BookId)
                                       .OrderByDescending(g => g.Count())
                                       .Select(g => new { bookId = g.Key, count = g.Count() })
                                       .FirstOrDefault();
        return result != null ? (result.bookId, result.count) : (null, 0);
    }
}
