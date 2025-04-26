using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Persistence.Domain.Reviews;

public class ProductReviewReplyValidator
{
    public const int MaxTextLength = 50_000;

    public static void Validate(ProductReviewReply reply)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(reply.Text))
            errors.Add(new ValidationError("Reply text cannot be empty."));

        if (reply.Text.Length > MaxTextLength)
            errors.Add(new ValidationError($"Reply text cannot be longer than {MaxTextLength} characters."));

        ResponseValidationException.ThrowIf(errors.Any, errors);
    }

    public static ProductReviewReply CreateValid(
        Guid userId,
        Guid reviewId,
        string text,
        DateTime createdAtUtc
    )
    {
        ProductReviewReply reply = new(userId, reviewId, text, createdAtUtc);

        Validate(reply);

        return reply;
    }
}