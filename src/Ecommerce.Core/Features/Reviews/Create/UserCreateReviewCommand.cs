namespace Ecommerce.Core.Features.Reviews.Create;

public record UserCreateReviewCommand(Guid ProductId, string Text);