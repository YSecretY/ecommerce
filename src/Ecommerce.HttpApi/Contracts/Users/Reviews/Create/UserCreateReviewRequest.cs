using System.Text.Json.Serialization;

namespace Ecommerce.HttpApi.Contracts.Users.Reviews.Create;

public class UserCreateReviewRequest
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}