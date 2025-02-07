using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations;

internal class LoanItemService : ILoanItemService
{
    public (int BookId, int Count) MostBorrowedBook()
    {
        ILoanItemRepository loanItemRepository = new LoanItemRepository();
        var data = loanItemRepository.MostBorrowedBook();
        _ = data.bookId ?? throw new NotFoundException("There is nothing to show here");
        return ((int)data.bookId, data.count);
    }
}
