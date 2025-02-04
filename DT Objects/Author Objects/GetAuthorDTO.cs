using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Helpers;

namespace Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;

public class GetAuthorDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<BasicBookDTO>? Books { get; set; }

    public override string ToString()
    {
        string result = $"{Id} - {Name}\r\n";
        if (Books != null && Books.Count != 0)
        {
            foreach (var b in Books)
            {
                result += b.ToString() + "\r\n";
            }
        }

        return result;
    }
}
