using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Reviews;
using Ecommerce.Core.Features.Reviews;

namespace Ecommerce.HttpApi.Contracts.Reviews;

public class ProductReviewResponse(Guid reviewId, Guid userId, string text, DateTime createdAt)
{
    public ProductReviewResponse(ProductReviewDto dto)
        : this(dto.ReviewId, dto.UserId, dto.Text, dto.CreatedAt)
    {
    }

    [JsonPropertyName("reviewId")]
    public Guid ReviewId { get; private set; } = reviewId;

    [JsonPropertyName("userId")]
    public Guid UserId { get; private set; } = userId;

    [JsonPropertyName("text")]
    public string Text { get; private set; } = text;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; private set; } = createdAt;
}