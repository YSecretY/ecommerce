namespace Ecommerce.Extensions.Exceptions;

public class ResponseException(string code, string message, object? additionalData = null) : Exception(message)
{
    public string Code { get; init; } = code;

    public object? AdditionalData { get; init; } = additionalData;
}