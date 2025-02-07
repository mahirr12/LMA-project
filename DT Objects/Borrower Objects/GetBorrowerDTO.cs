using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Borrower_Objects;

public class GetBorrowerDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<BasicLoanDTO> Loans { get; set; } = new();

    public override string ToString()
    {
        string result = $"{Id} - {Name}, {Email}";
        if (Loans.Where(l => l.ReturnDate==null).Any())
        {
            result += "\r\n Active loans: ";
            foreach (var loan in Loans.Where(l => l.ReturnDate == null))
            {
                result += "\r\n";
                result += " "+loan.ToString();
            }
        }
        return result;
    }
}
