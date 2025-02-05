using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Loan_Objects;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

public interface ILoanService
{
    void Create(CreateLoanDTO createLoanDTO);
    void Return(int id);
    List<GetLoanDTO> GetAll();
    GetLoanDTO GetById(int id);
}
