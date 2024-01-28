namespace Lesson_1_2.Contracts;

public class RotateException : Exception
{
    public RotateException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
