namespace Ecommerce.Core.Users.Reviews.Create;

public class UserCreateReviewCommand(Guid productId, string text)
{
    public Guid ProductId { get; private set; } = productId;

    public string Text { get; private set; } = text;
}