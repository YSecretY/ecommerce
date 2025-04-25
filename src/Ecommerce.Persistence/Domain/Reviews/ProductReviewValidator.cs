using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Persistence.Domain.Reviews;

public class ProductReviewValidator
{
    public const int MaxTextLength = 50_000;

    public static ValidationResult Validate(ProductReview review)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(review.Text))
            errors.Add(new ValidationError("Review text cannot be empty."));

        if (review.Text.Length > MaxTextLength)
            errors.Add(new ValidationError($"Review text cannot be longer than {MaxTextLength} characters."));

        return new ValidationResult(errors);
    }
}