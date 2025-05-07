using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;

namespace Ecommerce.HttpApi.Contracts.Products;

public class ProductWithViewsInfoResponse(ProductResponse product, int totalViews)
{
    [JsonPropertyName("product")]
    public ProductResponse Product { get; private set; } = product;

    [JsonPropertyName("viewsCount")]
    public int ViewsCount { get; private set; } = totalViews;

    public ProductWithViewsInfoResponse(ProductWithViewsInfoDto dto) :
        this(new ProductResponse(dto.Product), dto.ViewsCount)
    {
    }
}