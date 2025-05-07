namespace Ecommerce.Core.Abstractions.Analytics.Models.Products;

public class ProductSalesDto(Guid productId, int totalSold)
{
    public Guid ProductId { get; private set; } = productId;

    public int TotalSold { get; private set; } = totalSold;
}