namespace Project___ConsoleApp__Library_Management_Application_.AppExceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Not found")
    {

    }
    public NotFoundException(string? message) : base(message)
    {

    }
}
