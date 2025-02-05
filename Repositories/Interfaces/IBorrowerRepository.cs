using Project___ConsoleApp__Library_Management_Application_.Entitys;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

public interface IBorrowerRepository : IGenericRepository<Borrower>
{
    List<Borrower> GetAllWithLoans();
    Borrower? GetByIdWithLoans(int id);
}
