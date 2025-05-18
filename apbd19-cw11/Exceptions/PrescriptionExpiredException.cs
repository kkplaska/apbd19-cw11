namespace apbd19_cw11.Exceptions;

public class PrescriptionExpiredException : Exception
{
    public PrescriptionExpiredException()
    {
    }

    public PrescriptionExpiredException(string? message) : base(message)
    {
    }

    public PrescriptionExpiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}