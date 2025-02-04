namespace Project___ConsoleApp__Library_Management_Application_.Entitys
{

    public class Book : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PublishedYear { get; set; }
        public List<Author> Authors { get; set; } = new();
    }
}
