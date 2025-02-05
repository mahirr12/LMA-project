using Project___ConsoleApp__Library_Management_Application_.Entitys;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Book? GetByIdWithAuthors(int id);
    List<Book> GetAllWithAuthors();
}
