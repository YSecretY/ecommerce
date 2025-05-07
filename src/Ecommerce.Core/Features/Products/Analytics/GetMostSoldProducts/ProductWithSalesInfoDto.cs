using Ecommerce.Core.Abstractions.Models.Products;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;

public class ProductWithSalesInfoDto(ProductDto product, int soldCount)
{
    public ProductDto Product { get; private set; } = product;

    public int SoldCount { get; private set; } = soldCount;
}