namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{
    /*Loan:
Id (int, Primary Key)
BorrowerId (int, Foreign Key)
LoanDate (DateTime)
MustReturnDate(DateTime)
ReturnDate (DateTime?)
 */
    public class Loan : BaseEntity
    {
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; } = null!;
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<LoanItem> LoanItems { get; set; } = new();

        public Loan()
        {
            MustReturnDate = LoanDate.AddDays(15);
        }
    }
}
