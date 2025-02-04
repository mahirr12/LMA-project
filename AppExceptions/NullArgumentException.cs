namespace Project___ConsoleApp__Library_Management_Application_.AppExceptions;

public class NullArgumentException : Exception
{
    public NullArgumentException() : base("Arguments cannot be null.")
    {
    }

    public NullArgumentException(string? message) : base(message)
    {

    }
}
