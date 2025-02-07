namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

public interface ILoanItemService
{
    (int BookId, int Count) MostBorrowedBook();
}
