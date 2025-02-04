namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{
    /*LoanItem
      Id (int, Primary Key)
      BookId (int, Foreign Key)
    */
    public class LoanItem : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = null!;
    }
}
