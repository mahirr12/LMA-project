using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

public interface IAuthorService
{
    void Create(CreateAuthorDTO createAuthorDTO);
    void Delete(int id);
    List<GetAuthorDTO> GetAll();
    GetAuthorDTO GetById(int id);
    void Update(int id,CreateAuthorDTO updateAuthorDTO);
}
