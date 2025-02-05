using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Loan_Objects;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations;

public class LoanService : ILoanService
{
    public void Create(CreateLoanDTO createLoanDTO)
    {
        _ = createLoanDTO ?? throw new NullArgumentException();
        new BorrowerService().GetById(createLoanDTO.BorrowerId);

        var loan = new Loan { BorrowerId = createLoanDTO.BorrowerId };
        foreach (var bookId in createLoanDTO.BookIds)
        {
            loan.LoanItems.Add(new LoanItem { BookId = bookId });
        }
        ILoanRepository loanRepository = new LoanRepository();
        loanRepository.Add(loan);
        loanRepository.Commit();
    }

    public List<GetLoanDTO> GetAll()
    {
        ILoanRepository loanRepository = new LoanRepository();
        var loans = loanRepository.GetAllWithBorrowerAndBooks();
        if (loans.Count == 0) throw new NotFoundException("There is nothing to show here");

        var datas = new List<GetLoanDTO>();
        foreach (var loan in loans)
        {
            var basicBorrower = new BasicBorrowerDTO
            {
                Email = loan.Borrower.Email,
                Id = loan.BorrowerId,
                Name = loan.Borrower.Name
            };
            var loanBooks = new List<BasicBookDTO>();
            foreach (var item in loan.LoanItems)
            {
                loanBooks.Add(new BasicBookDTO
                {
                    Id = item.BookId,
                    Title = item.Book.Title
                });
            }
            datas.Add(new GetLoanDTO
            {
                Id = loan.Id,
                Borrower = basicBorrower,
                LoanBooks = loanBooks,
                LoanDate = loan.LoanDate,
                MustReturnDate = loan.MustReturnDate,
                ReturnDate = loan.ReturnDate
            });
        }
        return datas;
    }

    public GetLoanDTO GetById(int id)
    {
        ILoanRepository loanRepository = new LoanRepository();
        var loan = loanRepository.GetByIdWithBorrowerAndBooks(id) ?? throw new NotFoundException("Loan not found");

        var basicBorrower = new BasicBorrowerDTO
        {
            Email = loan.Borrower.Email,
            Id = loan.BorrowerId,
            Name = loan.Borrower.Name
        };
        var loanBooks = new List<BasicBookDTO>();
        foreach (var item in loan.LoanItems)
        {
            loanBooks.Add(new BasicBookDTO
            {
                Id = item.BookId,
                Title = item.Book.Title
            });
        }
        var data = new GetLoanDTO
        {
            Id = loan.Id,
            Borrower = basicBorrower,
            LoanBooks = loanBooks,
            LoanDate = loan.LoanDate,
            MustReturnDate = loan.MustReturnDate,
            ReturnDate = loan.ReturnDate
        };
        return data;

    }

    public void Return(int id)
    {
        ILoanRepository loanRepository = new LoanRepository();
        var loan = loanRepository.GetById(id) ?? throw new NotFoundException("Loan not found");

        loan.ReturnDate = DateTime.UtcNow.AddHours(4);
        loanRepository.Commit();
    }
}
