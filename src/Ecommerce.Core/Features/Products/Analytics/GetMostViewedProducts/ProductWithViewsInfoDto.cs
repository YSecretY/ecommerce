using Ecommerce.Core.Abstractions.Models.Products;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;

public class ProductWithViewsInfoDto(ProductDto product, int viewsCount)
{
    public ProductDto Product { get; private set; } = product;

    public int ViewsCount { get; private set; } = viewsCount;
}