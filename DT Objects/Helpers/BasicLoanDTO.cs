namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

public class BasicLoanDTO
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime MustReturnDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public List<BasicBookDTO> LoanBooks { get; set; } = null!;

    public override string ToString()
    {
        string result = $"{Id} - Loan date: {DateOnly.FromDateTime(LoanDate)} Must return at: {DateOnly.FromDateTime(MustReturnDate)} {(ReturnDate !=null ? $"Returned at: {DateOnly.FromDateTime((DateTime)ReturnDate)}" : "Status: Active")}: \r\n";
        foreach (var b in LoanBooks)
        {
            result += "  "+b.ToString() + "\r\n";
        }
        return result;
    }
}
