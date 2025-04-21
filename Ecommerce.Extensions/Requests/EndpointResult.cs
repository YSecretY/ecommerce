using System.Text.Json.Serialization;

namespace Ecommerce.Extensions.Requests;

public class EndpointResult<T>
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("errorCode")]
    public string? ErrorCode { get; init; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("data")]
    public T? Data { get; init; }
}