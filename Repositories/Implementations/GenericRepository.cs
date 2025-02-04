using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        public readonly AppDBContext _context;
        public GenericRepository()
        {
            _context = new AppDBContext();
        }


        //functions
        public void Add(T entity) 
        => _context.Set<T>().Add(entity);

        public void Remove(T entity) 
        => entity.IsDeleted = true;

        public List<T> GetAll()
        => _context.Set<T>()
            .Where(t => !t.IsDeleted)
            .ToList();

        public T? GetById(int id)
        => _context.Set<T>()
            .Where(t => !t.IsDeleted)
            .FirstOrDefault(t => t.Id == id);

        public int Commit()
        => _context.SaveChanges();
    }
}
