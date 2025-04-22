using System.Text.Json.Serialization;
using Ecommerce.Extensions.Types;

namespace Ecommerce.HttpApi.Contracts.Products.GetList;

public class ProductsListResponse(PaginatedResult<ProductResponse> products)
{
    public ProductsListResponse(PaginatedEnumerable<ProductResponse> enumerable)
        : this(new PaginatedResult<ProductResponse>(enumerable))
    {
    }

    [JsonPropertyName("products")]
    public PaginatedResult<ProductResponse> Products { get; private set; } = products;
}