using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Borrower_Objects;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

public interface IBorrowerService
{
    void Create(CreateBorrowerDTO createBorrowerDTO);
    void Delete(int id);
    void Update(int id, CreateBorrowerDTO updateBorrowerDTO);
    GetBorrowerDTO GetById(int id);
    List<GetBorrowerDTO> GetAll();
}
