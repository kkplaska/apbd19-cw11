namespace apbd19_cw11.Exceptions;

public class TooMuchMedicamentsException : Exception
{
    public TooMuchMedicamentsException()
    {
    }

    public TooMuchMedicamentsException(string? message) : base(message)
    {
    }

    public TooMuchMedicamentsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}