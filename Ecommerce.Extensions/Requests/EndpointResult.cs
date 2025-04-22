using System.Text.Json;
using System.Text.Json.Serialization;
using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Extensions.Requests;

public class EndpointResult<T>
{
    public EndpointResult(T data)
    {
        Data = data;
        IsSuccess = true;
    }

    public EndpointResult(ResponseException exception)
    {
        IsSuccess = false;
        ErrorCode = exception.Code;
        ErrorMessage = exception.Message;
        ErrorDetails = exception.AdditionalData;
    }

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("errorCode")]
    public string? ErrorCode { get; init; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("errorDetails")]
    public object? ErrorDetails { get; init; }

    [JsonPropertyName("data")]
    public T? Data { get; init; }
}