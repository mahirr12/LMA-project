using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public List<Book> GetAllWithAuthors()
        => _context.Books.Where(b => !b.IsDeleted)
                         .Include(b => b.Authors)
                         .ToList();


    public Book? GetByIdWithAuthors(int id) 
        => _context.Books.Include(b => b.Authors)
                         .Where(b => !b.IsDeleted)
                         .FirstOrDefault(b => b.Id == id);
}
