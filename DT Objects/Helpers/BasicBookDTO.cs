namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

public class BasicBookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public override string ToString()
    {
        return $"{Id} - {Title}";
    }
}
