namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Loan_Objects;

public class CreateLoanDTO
{
    public int BorrowerId { get; set; }
    public List<int> BookIds { get; set; } = null!;
}
