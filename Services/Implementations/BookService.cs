using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations;

public class BookService : IBookService
{
    public void Create(CreateBookDTO createBookDTO)
    {
        if (createBookDTO == null
            || string.IsNullOrWhiteSpace(createBookDTO.Title)
            || string.IsNullOrWhiteSpace(createBookDTO.Description)) throw new NullArgumentException();

        var book = new Book()
        {
            Title = createBookDTO.Title,
            Description = createBookDTO.Description,
            PublishedYear = createBookDTO.PublishedYear,
            Authors = new List<Author>()
        };

        IBookRepository bookRepository = new BookRepository();
        var authors = bookRepository.SetAuthors(createBookDTO.AuthorIds);
        if (authors is null || authors.Count < createBookDTO.AuthorIds.Count) throw new NotFoundException("Author(s) not found");

        foreach (var author in authors)
        {
            book.Authors.Add(author);
        }
        bookRepository.Add(book);

        bookRepository.Commit();
    }

    public void Delete(int id)
    {
        IBookRepository bookRepository = new BookRepository();
        var book = bookRepository.GetById(id) ?? throw new NotFoundException("Book not found");
        bookRepository.Remove(book);
        bookRepository.Commit();
    }

    public List<GetBookDTO> GetAll()
    {
        IBookRepository bookRepository = new BookRepository();
        var books = bookRepository.GetAllWithAuthors();
        if (books.Count == 0) throw new NotFoundException("There is nothing to show here");

        var datas = new List<GetBookDTO>();
        foreach (var book in books)
        {
            var basicAuthors = new List<BasicAuthorDTO>();
            foreach (var author in book.Authors) basicAuthors.Add(new BasicAuthorDTO()
            {
                Id = author.Id,
                Name = author.Name
            });

            datas.Add(new GetBookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedYear = book.PublishedYear,
                Authors = basicAuthors,
                IsAvailable = bookRepository.IsAvailable(book.Id)
            });
        }

        return datas;
    }

    public GetBookDTO GetById(int id)
    {
        IBookRepository bookRepository = new BookRepository();
        var book = bookRepository.GetByIdWithAuthors(id) ?? throw new NotFoundException("Book not found");
        var basicAuthors = new List<BasicAuthorDTO>();
        foreach (var author in book.Authors) basicAuthors.Add(new BasicAuthorDTO()
        {
            Id = author.Id,
            Name = author.Name
        });
        GetBookDTO getBookDTO = new GetBookDTO()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            PublishedYear = book.PublishedYear,
            Authors = basicAuthors,
            IsAvailable = bookRepository.IsAvailable(book.Id)
        };

        return getBookDTO;
    }

    public void Update(int id, CreateBookDTO updateBookDTO)
    {
        if (updateBookDTO == null
            || string.IsNullOrWhiteSpace(updateBookDTO.Title)
            || string.IsNullOrWhiteSpace(updateBookDTO.Description)) throw new NullArgumentException();

        IBookRepository bookRepository = new BookRepository();
        var book = bookRepository.GetByIdWithAuthors(id) ?? throw new NotFoundException("Book not found");

        book.Title = updateBookDTO.Title;
        book.Description = updateBookDTO.Description;
        book.PublishedYear = updateBookDTO.PublishedYear;
        book.UpdatedTime = DateTime.UtcNow.AddHours(4);
        book.Authors.Clear();

        var authors = bookRepository.SetAuthors(updateBookDTO.AuthorIds);
        if (authors is null || authors.Count < updateBookDTO.AuthorIds.Count) throw new NotFoundException("Author(s) not found");

        foreach (var author in authors)
        {
            book.Authors.Add(author);
        }
        bookRepository.Commit();
    }
}
