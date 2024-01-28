namespace Lesson_1_2.Contracts;

public class MoveException : Exception
{
    public MoveException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
