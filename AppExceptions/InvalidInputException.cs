namespace Project___ConsoleApp__Library_Management_Application_.AppExceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException() : base("Invalid input!")
    {

    }
    public InvalidInputException(string message) : base(message)
    {

    }
}
