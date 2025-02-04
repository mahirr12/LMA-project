namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{
    public class Borrower : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<Loan>? Loans { get; set; }
    }
}
