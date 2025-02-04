namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;

public class CreateBookDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PublishedYear { get; set; }
    public List<int> AuthorIds { get; set; } = null!;
}
