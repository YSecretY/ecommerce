namespace Ecommerce.Core.Features.Users.Reviews.Create;

public record UserCreateReviewCommand(Guid ProductId, string Text);