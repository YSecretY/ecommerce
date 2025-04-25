namespace Ecommerce.Extensions.Exceptions;

public class ForbiddenException() : ResponseException("Unauthorized", "Forbidden.")
{
    public ForbiddenException(object? additionalData) : this() =>
        AdditionalData = additionalData;

    public static void ThrowIf(Func<bool> condition, object? data = null)
    {
        if (condition())
            throw new ResponseValidationException(data ?? "Validation failed.");
    }

    public static void ThrowIf(bool condition, object? data = null)
    {
        if (condition)
            throw new ResponseValidationException(data ?? "Validation failed.");
    }
}