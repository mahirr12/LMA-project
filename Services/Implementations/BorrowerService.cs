using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Borrower_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations;

public class BorrowerService : IBorrowerService
{
    public void Create(CreateBorrowerDTO createBorrowerDTO)
    {
        if (createBorrowerDTO == null
            || string.IsNullOrWhiteSpace(createBorrowerDTO.Email)
            || string.IsNullOrWhiteSpace(createBorrowerDTO.Name)) throw new ArgumentNullException();

        var borrower = new Borrower
        {
            Name = createBorrowerDTO.Name,
            Email = createBorrowerDTO.Email
        };

        IBorrowerRepository borrowerRepository = new BorrowerRepository();
        borrowerRepository.Add(borrower);
        borrowerRepository.Commit();
    }

    public void Delete(int id)
    {
        IBorrowerRepository borrowerRepository = new BorrowerRepository();
        var borrower = borrowerRepository.GetById(id) ?? throw new NotFoundException("Borrower not found");
        borrowerRepository.Remove(borrower);
        borrowerRepository.Commit();
    }

    public List<GetBorrowerDTO> GetAll()
    {
        IBorrowerRepository borrowerRepository = new BorrowerRepository();
        var borrowers = borrowerRepository.GetAllWithLoans();
        if (borrowers is null || borrowers.Count == 0) throw new NotFoundException("There is nothing to show here");

        var datas = new List<GetBorrowerDTO>();

        foreach (var borrower in borrowers)
        {
            var basicLoans = new List<BasicLoanDTO>();
            if (borrower.Loans != null && borrower.Loans.Count != 0)
            {
                foreach (var loan in borrower.Loans)
                {
                    var loanBooks = new List<BasicBookDTO>();
                    if (loan.LoanItems != null && loan.LoanItems.Count != 0)
                    {
                        foreach (var item in loan.LoanItems)
                        {
                            loanBooks.Add(new BasicBookDTO { Id = item.Book.Id, Title = item.Book.Title });
                        };
                    }

                    basicLoans.Add(new BasicLoanDTO
                    {
                        Id = loan.Id,
                        LoanDate = loan.LoanDate,
                        MustReturnDate = loan.MustReturnDate,
                        ReturnDate = loan.ReturnDate,
                        LoanBooks = loanBooks
                    });
                }
            }

            datas.Add(new GetBorrowerDTO
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Email = borrower.Email,
                Loans = basicLoans
            });
        }
        return datas;
    }

    public GetBorrowerDTO GetById(int id)
    {
        IBorrowerRepository borrowerRepository = new BorrowerRepository();
        var borrower = borrowerRepository.GetByIdWithLoans(id)
                       ?? throw new NotFoundException("Borrower not found");

        var basicLoans = new List<BasicLoanDTO>();
        if (borrower.Loans != null && borrower.Loans.Count != 0)
        {
            foreach (var loan in borrower.Loans)
            {
                var loanBooks = new List<BasicBookDTO>();
                if (loan.LoanItems != null && loan.LoanItems.Count != 0)
                {
                    foreach (var item in loan.LoanItems)
                    {
                        loanBooks.Add(new BasicBookDTO { Id = item.Book.Id, Title = item.Book.Title });
                    };
                }

                basicLoans.Add(new BasicLoanDTO
                {
                    Id = loan.Id,
                    LoanDate = loan.LoanDate,
                    MustReturnDate = loan.MustReturnDate,
                    ReturnDate = loan.ReturnDate,
                    LoanBooks = loanBooks
                });
            }
        }

        var getBorrowerDTO = new GetBorrowerDTO
        {
            Id = borrower.Id,
            Email = borrower.Email,
            Name = borrower.Name,
            Loans = basicLoans
        };
        return getBorrowerDTO;
    }

    public void Update(int id, CreateBorrowerDTO updateBorrowerDTO)
    {
        if (updateBorrowerDTO == null || updateBorrowerDTO.Email == null || updateBorrowerDTO.Name == null) throw new NullArgumentException();

        IBorrowerRepository borrowerRepository = new BorrowerRepository();
        var borrower = borrowerRepository.GetById(id) ?? throw new NotFoundException("Borrower not found");

        borrower.Name = updateBorrowerDTO.Name;
        borrower.Email = updateBorrowerDTO.Email;
        borrower.UpdatedTime = DateTime.UtcNow.AddHours(4);

        borrowerRepository.Commit();
    }
}
