namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;

public class CreateBookDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int PublishedYear { get; set; }
    public List<int> AuthorIds { get; set; } = new();
}
