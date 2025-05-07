using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;

namespace Ecommerce.HttpApi.Contracts.Products;

public class ProductWithSalesInfoResponse(ProductResponse product, int totalSold)
{
    [JsonPropertyName("product")]
    public ProductResponse Product { get; private set; } = product;

    [JsonPropertyName("soldCount")]
    public int SoldCount { get; private set; } = totalSold;

    public ProductWithSalesInfoResponse(ProductWithSalesInfoDto dto) :
        this(new ProductResponse(dto.Product), dto.SoldCount)
    {
    }
}