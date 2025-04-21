using System.Text.Json.Serialization;

namespace Ecommerce.Extensions.Exceptions;

public class ValidationResult(List<ValidationError> errors)
{
    [JsonPropertyName("errors")]
    public List<ValidationError> Errors { get; private set; } = errors;

    [JsonIgnore]
    public bool Success => Errors.Count is 0;

    [JsonIgnore]
    public bool Failed => Errors.Count > 0;
}