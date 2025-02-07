using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;

public class GetBookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PublishedYear { get; set; }
    public List<BasicAuthorDTO> Authors { get; set; } = null!;
    public bool IsAvailable { get; set; }

    public override string ToString()
    {
        
        return $"{Id} - {Title}. {PublishedYear} by {string.Join(", ",Authors.Select(a=>a.Name))}:\r\n {Description}";
    }
}
