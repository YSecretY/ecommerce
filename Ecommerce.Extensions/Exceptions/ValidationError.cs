namespace Ecommerce.Extensions.Exceptions;

public class ValidationError(string message)
{
    public string Message { get; private set; } = message;
}