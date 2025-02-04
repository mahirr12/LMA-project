using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Author_Objects;
using Project___ConsoleApp__Library_Management_Application_.DT_Objects.Book_Objects;
using Project___ConsoleApp__Library_Management_Application_.Services.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Services.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new BookService();
            var book = new CreateBookDTO { Title = "TestBookTest", Description = "This is test bookTest", PublishedYear = 2011, AuthorIds = new() { 2 } };
            bookService.Update(1, book);
            

        }
    }
}
