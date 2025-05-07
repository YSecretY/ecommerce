namespace Ecommerce.Core.Abstractions.Analytics.Models.Products;

public class ProductViewsDto(Guid productId, int viewsCount)
{
    public Guid ProductId { get; private set; } = productId;

    public int ViewsCount { get; private set; } = viewsCount;
}