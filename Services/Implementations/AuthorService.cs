using Project___ConsoleApp__Library_Management_Application_.AppExceptions;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Entitys;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repositories.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Services.Implementations;

public class AuthorService : IAuthorService
{
    public void Create(CreateAuthorDTO createAuthorDTO)
    {
        if (createAuthorDTO == null || string.IsNullOrWhiteSpace(createAuthorDTO.Name)) throw new NullArgumentException();
        IAuthorRepository authorRepository = new AuthorRepository();
        Author author = new Author()
        {
            Name = createAuthorDTO.Name,
            CreatedTime = DateTime.UtcNow.AddHours(4)
        };
        authorRepository.Add(author);
        authorRepository.Commit();
    }

    public void Delete(int id)
    {
        IAuthorRepository authorRepository = new AuthorRepository();
        var author = authorRepository.GetById(id) ?? throw new NotFoundException("Author not found");

        authorRepository.Remove(author);
        authorRepository.Commit();
    }

    public List<GetAuthorDTO> GetAll()
    {
        IAuthorRepository authorRepository = new AuthorRepository();
        var authors = authorRepository.GetAllWithBooks();
        if (authors == null || authors.Count == 0) throw new NotFoundException("There is nothing to show here");

        var datas = new List<GetAuthorDTO>();
        foreach (var author in authors)
        {
            var basicBooks = new List<BasicBookDTO>();
            if (author.Books != null)
            {
                foreach (var book in author.Books) basicBooks.Add(new BasicBookDTO()
                {
                    Id = book.Id,
                    Title = book.Title
                });
            }

            datas.Add(new GetAuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Books = basicBooks
            });
        }

        return datas;
    }

    public GetAuthorDTO GetById(int id)
    {
        IAuthorRepository authorsRepository = new AuthorRepository();
        var author = authorsRepository.GetByIdWithBooks(id) ?? throw new NotFoundException("Author not found");

        var basicBooks = new List<BasicBookDTO>();
        if (author.Books != null)
        {
            foreach (var book in author.Books) basicBooks.Add(new BasicBookDTO()
            {
                Id = book.Id,
                Title = book.Title
            });
        }

        GetAuthorDTO getAuthorDTO = new GetAuthorDTO()
        {
            Id = author.Id,
            Name = author.Name,
            Books = basicBooks
        };

        return getAuthorDTO;
    }

    public void Update(int id, CreateAuthorDTO updateAuthorDTO)
    {
        if (updateAuthorDTO == null || string.IsNullOrWhiteSpace(updateAuthorDTO.Name)) throw new NullArgumentException();

        IAuthorRepository authorRepository = new AuthorRepository();
        var author = authorRepository.GetById(id) ?? throw new NotFoundException("Author not found");

        author.Name = updateAuthorDTO.Name;
        author.UpdatedTime = DateTime.UtcNow.AddHours(4);
        authorRepository.Commit();
    }
}
