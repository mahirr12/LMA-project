namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

public class BasicLoanDTO
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime MustReturnDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public List<BasicLoanItemDTO> LoanItems { get; set; }
}
