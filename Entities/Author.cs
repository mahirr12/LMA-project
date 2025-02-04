namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = null!;
        public List<Book> Books { get; set; } = new();
    }
}
