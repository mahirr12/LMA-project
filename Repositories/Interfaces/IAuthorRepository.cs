using Project___ConsoleApp__Library_Management_Application_.Entitys;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    List<Author>? GetAllWithBooks();
    Author? GetByIdWithBooks(int id);
}
