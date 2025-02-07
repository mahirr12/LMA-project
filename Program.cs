using Azure;
using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Borrower_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Loan_Objects;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;
using System.Net;

namespace Project___ConsoleApp__Library_Management_Application_;

internal class Program
{
    static void Main(string[] args)
    {
        bool menu = true;
        do
        {
            Console.Clear();
        MainMenu:
            int n = 1;

            Console.WriteLine($"{n++} - Author actions");
            Console.WriteLine($"{n++} - Book actions");
            Console.WriteLine($"{n++} - Borrower actions");
            Console.WriteLine($"{n++} - Borrow book");
            Console.WriteLine($"{n++} - Return book");
            Console.WriteLine($"{n++} - Most borrowed book");
            Console.WriteLine($"{n++} - Overdue borrowers' list");
            Console.WriteLine($"{n++} - Borrowers' loan history");
            Console.WriteLine($"{n++} - Search books by title");
            Console.WriteLine($"{n++} - Search books by author");
            Console.WriteLine("\r\n0 - Exit\r\n");

            Console.Write("Select: ");
            var input = Console.ReadLine();


            switch (input)
            {
                case "1":
                AuthorActions:
                    try
                    {
                        AuthorActions();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                        goto AuthorActions;
                    }
                    break;
                case "2":
                BookActions:
                    try
                    {
                        BookActions();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                        goto BookActions;
                    }
                    break;
                case "3":
                BorrowerActions:
                    try
                    {
                        BorrowerActions();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                        goto BorrowerActions;
                    }
                    break;
                case "4":
                BorrowBook:
                    try
                    {
                        BorrowBook();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                        if (ex.Message == "There is nothing to show here") break;
                        goto BorrowBook;
                    }
                    break;
                case "5":
                ReturnBook:
                    try
                    {
                        ReturnBook();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                        if (ex.Message == "There is nothing to show here") break;
                        goto ReturnBook;
                    }
                    break;
                case "6":
                    try
                    {
                        Console.Clear();
                        ILoanItemService loanItemService = new LoanItemService();
                        var data = loanItemService.MostBorrowedBook();
                        IBookService bookService = new BookService();
                        var book = bookService.GetById(data.BookId);
                        book.Description = string.Empty;

                        Console.WriteLine(book + "" + "\r\n" + data.Count + " times borrowed");
                        Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                    }
                    break;
                case "7":
                    try
                    {
                        IBorrowerService service = new BorrowerService();
                        var borrower = service.GetAll()
                                              .Where(b => b.Loans
                                              .Any(l => l.MustReturnDate > DateTime.UtcNow.AddHours(4)
                                              && l.ReturnDate is null));
                        if (!borrower.Any()) throw new NotFoundException("There is nothing to show here");
                        Console.WriteLine("Overdue borrowers: ");
                        foreach (var b in borrower)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                    }
                    break;
                case "8":
                    try
                    {
                        Console.Clear();
                        IBorrowerService borrowerService = new BorrowerService();
                        var datas = borrowerService.GetAll();
                        foreach (var data in datas)
                        {
                            var loans = data.Loans;
                            data.Loans = new();
                            Console.WriteLine(data);
                            loans.ForEach(l => Console.WriteLine(" " + l));
                        }
                        Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                    }
                    break;
                case "9":
                    try
                    {
                        Console.Clear();
                        Console.Write("Search by title: ");
                        var keyWord = Console.ReadLine() ?? throw new NullArgumentException();
                        IBookService bookService = new BookService();
                        var datas = bookService.GetAll()
                                               .Where(b => b.Title.Trim().ToLower()
                                               .Contains(keyWord.Trim().ToLower())).ToList();
                        if (datas.Count == 0) throw new NotFoundException("Book not found");
                        Console.Clear();
                        foreach (var data in datas)
                        {
                            data.Description = string.Empty;
                            Console.WriteLine(data);
                        }
                        Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                    }
                    break;
                case "10":
                    try
                    {
                        Console.Clear();
                        Console.Write("Search by author: ");
                        var keyWord = Console.ReadLine() ?? throw new NullArgumentException();
                        IAuthorService service = new AuthorService();
                        var authors = service.GetAll()
                                             .Where(a => a.Name.Trim().ToLower()
                                             .Contains(keyWord.Trim().ToLower())).ToList();
                        var books = new List<BasicBookDTO>();
                        authors.ForEach(a => books.AddRange(a.Books));
                        books = books.Distinct().ToList();
                        if (books.Count == 0) throw new NotFoundException("Book not found");
                        books.ForEach(b => Console.WriteLine(b));

                        Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Wait();
                    }
                    break;
                case "0":
                    menu = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select from menu!!!\r\n");
                    goto MainMenu;
            }
        } while (menu);
    }
    static void Wait()
    {

        Console.WriteLine("\r\npress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    static void AuthorActions()
    {
        IAuthorService service = new AuthorService();
        Console.Clear();
    AuthorsMenu:
        int n = 1;
        Console.WriteLine($"{n++} - All authors");
        Console.WriteLine($"{n++} - Add author");
        Console.WriteLine($"{n++} - Edit author");
        Console.WriteLine($"{n++} - Delete author");
        Console.WriteLine("\r\n0 - Back\r\n");

        Console.Write("Select:");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Authors: ");

                var authors = service.GetAll();
                foreach (var author in authors)
                {
                    Console.WriteLine(author + "\r\n");
                }
                Wait();
                break;
            case "2":
                Console.Clear();
            CreateAuthor:
                Console.Write("Enter name: ");
                var createAuthor = new CreateAuthorDTO { Name = Console.ReadLine() };
                try { service.Create(createAuthor); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto CreateAuthor; }
                Console.WriteLine("\r\nAuthor added");
                Wait();
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Authors: ");
                authors = service.GetAll();
                foreach (var author in authors)
                {
                    author.Books.Clear();
                    Console.WriteLine(author);
                }
            UpdateAuthor:
                Console.Write("Select author's id: ");
                bool parse = int.TryParse(Console.ReadLine(), out int authorId);
                if (!parse) { Console.WriteLine("Invalid input"); goto UpdateAuthor; }
                try { service.GetById(authorId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto UpdateAuthor; }
                var updateAuthor = new CreateAuthorDTO();
                Console.Write("Enter name: ");
                updateAuthor.Name = Console.ReadLine();
                try { service.Update(authorId, updateAuthor); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto UpdateAuthor; }
                Console.WriteLine("\r\nAuhtor updated");
                Wait();
                break;
            case "4":
                Console.Clear();
                authors = service.GetAll();
                foreach (var author in authors)
                {
                    author.Books.Clear();
                    Console.WriteLine(author);
                }
            DeleteAuthor:
                Console.Write("Select author's id: ");
                parse = int.TryParse(Console.ReadLine(), out authorId);
                if (!parse) { Console.WriteLine("Invalid input"); goto DeleteAuthor; }
                try { service.GetById(authorId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto DeleteAuthor; }
                service.Delete(authorId);
                Console.WriteLine("\r\nAuthor deleted");
                Wait();
                break;
            case "0":
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please select from menu!!!\r\n");
                goto AuthorsMenu;
        }

    }
    static void BookActions()
    {
        IBookService service = new BookService();
        IAuthorService authorService = new AuthorService();
        Console.Clear();
    BooksMenu:
        int n = 1;
        Console.WriteLine($"{n++} - All books");
        Console.WriteLine($"{n++} - Add book");
        Console.WriteLine($"{n++} - Edit book");
        Console.WriteLine($"{n++} - Delete book");
        Console.WriteLine("\r\n0 - Back\r\n");

        Console.Write("Select:");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Books: ");

                var books = service.GetAll();
                foreach (var book in books)
                {
                    Console.WriteLine(book + "\r\n");
                }
                Wait();
                break;
            case "2":
                Console.Clear();
            CreateBook:
                Console.Write("Enter title: ");
                var title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title)) { Console.WriteLine("Invalid input"); goto CreateBook; }

            EnterDescription:
                Console.Write("Enter description: ");
                var desc = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(desc)) { Console.WriteLine("Invalid input"); goto EnterDescription; }

            EnterPublishedYear:
                Console.Write("Enter published year: ");
                bool parse = int.TryParse(Console.ReadLine(), out int publishedYear);
                if (!parse) { Console.WriteLine("Invalid input"); goto EnterPublishedYear; }

                foreach (var author in new AuthorService().GetAll())
                {
                    author.Books.Clear();
                    Console.WriteLine(author);
                }
            SetAuthors:
                var authorIds = new List<int>();
                Console.Write("Select author(s) with ',': ");
                var stringIds = Console.ReadLine() ?? string.Empty;
                foreach (var stringId in stringIds.Split(','))
                    if (int.TryParse(stringId.Trim(), out int id)) authorIds.Add(id);
                if (!authorIds.Any()) { Console.WriteLine("Invalid input"); goto SetAuthors; }
                authorIds = authorIds.Distinct().ToList();
                if (authorService.GetAll().Where(a => authorIds.Contains(a.Id)).Count() != authorIds.Count())
                {
                    Console.WriteLine("Some auhtor(s) not found");
                    goto SetAuthors;
                }
                Console.WriteLine($"{title}. {publishedYear} author(s): {string.Join(", ", authorIds)}\r\n {desc}");
                Wait();

                service.Create(new CreateBookDTO
                {
                    AuthorIds = authorIds,
                    Description = desc,
                    PublishedYear = publishedYear,
                    Title = title
                });

                Console.WriteLine("\r\nBook added");
                Wait();
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Books: ");

                books = service.GetAll();
                foreach (var book in books)
                {
                    book.Description = string.Empty;
                    Console.WriteLine(book + "\r\n");
                }
            UpdateBook:
                Console.Write("Select book's id: ");
                parse = int.TryParse(Console.ReadLine(), out int bookId);
                if (!parse) { Console.WriteLine("Invalid input"); goto UpdateBook; }
                try { service.GetById(bookId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto UpdateBook; }
                var updateBook = new CreateBookDTO();
                Console.Write("Enter title: ");
                var newTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newTitle)) { Console.WriteLine("Invalid input"); goto CreateBook; }

            UpdateDescription:
                Console.Write("Enter description: ");
                var newDesc = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDesc)) { Console.WriteLine("Invalid input"); goto UpdateDescription; }

            UpdatePublishedYear:
                Console.Write("Enter published year: ");
                parse = int.TryParse(Console.ReadLine(), out int newPublishedYear);
                if (!parse) { Console.WriteLine("Invalid input"); goto UpdatePublishedYear; }

                foreach (var author in new AuthorService().GetAll())
                {
                    author.Books.Clear();
                    Console.WriteLine(author);
                }
            UpdateAuthors:
                var newAuthorIds = new List<int>();
                Console.Write("Select author(s) with ',': ");
                var newStringIds = Console.ReadLine() ?? string.Empty;
                foreach (var stringId in newStringIds.Split(','))
                    if (int.TryParse(stringId.Trim(), out int id)) newAuthorIds.Add(id);
                if (!newAuthorIds.Any()) { Console.WriteLine("Invalid input"); goto UpdateAuthors; }
                newAuthorIds = newAuthorIds.Distinct().ToList();
                Console.WriteLine($"{newTitle}. {newPublishedYear} author(s): {string.Join(", ", newAuthorIds)}\r\n {newDesc}");
                Wait();

                service.Update(bookId, new CreateBookDTO
                {
                    AuthorIds = newAuthorIds,
                    Description = newDesc,
                    PublishedYear = newPublishedYear,
                    Title = newTitle
                });
                Console.WriteLine("\r\nBook updated");
                Wait();
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("Books: ");
                books = service.GetAll();
                foreach (var book in books)
                {
                    book.Description = string.Empty;
                    Console.WriteLine(book + "\r\n");
                }
            DeleteBook:
                Console.Write("Select book's id: ");
                parse = int.TryParse(Console.ReadLine(), out bookId);
                if (!parse) { Console.WriteLine("Invalid input"); goto DeleteBook; }
                try { service.GetById(bookId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto DeleteBook; }
                service.Delete(bookId);
                Console.WriteLine("\r\nBook deleted");
                Wait();
                break;
            case "0":
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please select from menu!!!\r\n");
                goto BooksMenu;

        }
    }
    static void BorrowerActions()
    {
        IBorrowerService service = new BorrowerService();
        Console.Clear();
    BorrowersMenu:
        int n = 1;
        Console.WriteLine($"{n++} - All borrowers");
        Console.WriteLine($"{n++} - Add borrower");
        Console.WriteLine($"{n++} - Edit borrower");
        Console.WriteLine($"{n++} - Delete borrower");
        Console.WriteLine("\r\n0 - Back\r\n");

        Console.Write("Select:");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                Console.WriteLine("Borrowers: ");

                var borrowers = service.GetAll();
                foreach (var borrower in borrowers)
                {
                    Console.WriteLine(borrower + "\r\n");
                }
                Wait();
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("Enter name: ");
                var name = Console.ReadLine();
                Console.WriteLine("Enter e-mail: ");
                var email = Console.ReadLine();
                service.Create(new CreateBorrowerDTO
                {
                    Name = name,
                    Email = email
                });
                Console.WriteLine("\r\nBorrower added");
                Wait();
                break;
            case "3":
                Console.Clear();
                Console.WriteLine("Borrowers: ");
                borrowers = service.GetAll();
                foreach (var borrower in borrowers)
                {
                    borrower.Loans.Clear();
                    Console.WriteLine(borrower + "\r\n");
                }
            UpdateBorrower:
                Console.Write("Select borrower's id: ");
                var parse = int.TryParse(Console.ReadLine(), out int borrowerId);
                if (!parse) { Console.WriteLine("Invalid input"); goto UpdateBorrower; }
                try { service.GetById(borrowerId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto UpdateBorrower; }
                Console.WriteLine("Enter name: ");
                var newName = Console.ReadLine();
                Console.WriteLine("Enter e-mail: ");
                var newEmail = Console.ReadLine();
                service.Update(borrowerId, new CreateBorrowerDTO
                {
                    Name = newName,
                    Email = newEmail
                });
                Console.WriteLine("\r\nBorrower updated");
                Wait();
                break;
            case "4":
                Console.Clear();
                Console.WriteLine("Borrowers: ");
                borrowers = service.GetAll();
                foreach (var borrower in borrowers)
                {
                    borrower.Loans.Clear();
                    Console.WriteLine(borrower + "\r\n");
                }
            DeleteBorrower:
                Console.WriteLine("Select borrower's id: ");
                parse = int.TryParse(Console.ReadLine(), out borrowerId);
                if (!parse) { Console.WriteLine("Invalid input"); goto DeleteBorrower; }
                try { service.GetById(borrowerId); }
                catch (Exception ex) { Console.WriteLine(ex.Message); goto DeleteBorrower; }
                service.Delete(borrowerId);
                Console.WriteLine("\r\nBorrower deleted");
                Wait();
                break;
            case "0":
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Please select from menu!!!\r\n");
                goto BorrowersMenu;

        }
    }
    static void BorrowBook()
    {
        ILoanService loanService = new LoanService();
        IBookService bookService = new BookService();
        IBorrowerService borrowerService = new BorrowerService();
        Console.Clear();
        var books = bookService.GetAll();
        foreach (var book in books)
        {
            book.Description = string.Empty;
            Console.WriteLine(book + (!book.IsAvailable ? "(Not available)" : string.Empty) + "\r\n");
        }
    BorrowBook:
        var bookIds = new List<int>();
        Console.Write("Select book(s) with ',': ");
        var stringIds = Console.ReadLine() ?? string.Empty;
        foreach (var stringId in stringIds.Split(','))
            if (int.TryParse(stringId.Trim(), out int id)) bookIds.Add(id);
        bookIds = bookIds.Distinct().ToList();
        try { foreach (var id in bookIds) bookService.GetById(id); }
        catch (Exception)
        {
            Console.WriteLine("Some book(s) not found"); goto BorrowBook;
        }

        if (books.Where(b => bookIds.Contains(b.Id) && !b.IsAvailable).ToList().Any())
        {
            Console.WriteLine("Some book(s) not available");
            goto BorrowBook;
        }
        if (!bookIds.Any()) { Console.WriteLine("Invalid input"); goto BorrowBook; }

        Console.Clear();
        Console.WriteLine("Borrowers: ");
        var borrowers = borrowerService.GetAll();
        foreach (var borrower in borrowers)
        {
            borrower.Loans.Clear();
            Console.WriteLine(borrower + "\r\n");
        }
        Console.Write("Selected book(s): " + string.Join(", ", bookIds));

    SelectBorrower:
        Console.Write("\r\nSelect borrower's id: ");
        if (!int.TryParse(Console.ReadLine(), out int borrowerId))
        { Console.WriteLine("Invalid input"); goto SelectBorrower; }
        try { borrowerService.GetById(borrowerId); }
        catch (Exception ex) { Console.WriteLine(ex.Message); goto SelectBorrower; }
        loanService.Create(new CreateLoanDTO
        {
            BookIds = bookIds,
            BorrowerId = borrowerId
        });
        Console.WriteLine("\r\nBorrowed book(s)");
        Wait();
    }
    static void ReturnBook()
    {
        IBorrowerService borrowerService = new BorrowerService();
        ILoanService loanService = new LoanService();
        Console.Clear();


        var borrowers = borrowerService.GetAll().Where(b => b.Loans.Where(l => l.ReturnDate == null).Any());
        if (!borrowers.Any()) throw new NotFoundException("There is nothing to show here");
        Console.WriteLine("Borrowers: ");
        foreach (var borrower in borrowers)
        {
            Console.WriteLine(borrower + "\r\n");
        }
        Console.Write("Select loan's id: ");
        if (!int.TryParse(Console.ReadLine(), out int loanId)) throw new InvalidInputException();
        loanService.Return(loanId);
        Console.WriteLine("\r\n Book(s) returned");
        Wait();

    }
}
