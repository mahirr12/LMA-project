using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Borrower_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Loan_Objects;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Services.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IAuthorService authorService = new AuthorService();
            //authorService.Create(new CreateAuthorDTO()
            //{
            //    Name= "Miguel de Cervantes",
            //});

            //IBookService bookService = new BookService();
            //bookService.Create(new CreateBookDTO
            //{
            //    AuthorIds = new() { 1 },
            //    Title = "Don Quixote de la Mancha",
            //    PublishedYear = 1605,
            //    Description = "Don Quixote was originally written as a parody of the chivalric romances that were popular at the time of its publication, in the early 1600s. It realistically describes what happens to an aging knight who has been misled by the romances he has read; the titular Don Quixote sets out on his old horse to seek adventure, along with his squire Sancho Panza."
            //});

            //IBorrowerService borrowerService = new BorrowerService();
            //borrowerService.Create(new CreateBorrowerDTO { Email="mahir.cumaliyev@icloud.com",Name="Mahir Cumaliyev"});

            ILoanService service = new LoanService();
            service.Create(new CreateLoanDTO
            {
                BookIds = new List<int> { 1 },
                BorrowerId = 1
            });
        }
    }
}
