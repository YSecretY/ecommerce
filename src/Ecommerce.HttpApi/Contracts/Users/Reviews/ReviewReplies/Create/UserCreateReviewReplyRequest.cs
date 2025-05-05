using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Replies.Create;

namespace Ecommerce.HttpApi.Contracts.Users.Reviews.ReviewReplies.Create;

public class UserCreateReviewReplyRequest
{
    [JsonPropertyName("reviewId")]
    public Guid ReviewId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    public UserCreateReviewReplyCommand ToCommand() =>
        new(ReviewId, Text);
}