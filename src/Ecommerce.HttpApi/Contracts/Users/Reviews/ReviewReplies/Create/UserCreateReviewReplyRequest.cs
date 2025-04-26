using System.Text.Json.Serialization;

namespace Ecommerce.HttpApi.Contracts.Users.Reviews.ReviewReplies.Create;

public record UserCreateReviewReplyRequest(
    [property: JsonPropertyName("reviewId")]
    Guid ReviewId,
    [property: JsonPropertyName("text")]
    string Text
);