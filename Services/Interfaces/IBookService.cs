using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

public interface IBookService
{
    void Create(CreateBookDTO createBookDTO);
    void Delete(int id);
    GetBookDTO GetById(int id);
    List<GetBookDTO> GetAll();
    void Update(int id,CreateBookDTO updateBookDTO);
}
