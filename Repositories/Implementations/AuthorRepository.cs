using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    public List<Author>? GetAllWithBooks()
        => _context.Authors.Where(a => !a.IsDeleted)
                           .Include(a => a.Books)
                           .ToList();

    public Author? GetByIdWithBooks(int id)
        => _context.Authors.Where(a => !a.IsDeleted)
                           .Include(a => a.Books)
                           .FirstOrDefault(a => a.Id == id);
}
