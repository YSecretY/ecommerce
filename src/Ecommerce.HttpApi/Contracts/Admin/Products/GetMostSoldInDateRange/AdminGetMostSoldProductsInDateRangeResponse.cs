using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts.Products;

namespace Ecommerce.HttpApi.Contracts.Admin.Products.GetMostSoldInDateRange;

public class AdminGetMostSoldProductsInDateRangeResponse(PaginatedResult<ProductWithSalesInfoResponse> products)
{
    public AdminGetMostSoldProductsInDateRangeResponse(PaginatedEnumerable<ProductWithSalesInfoDto> dtoProducts) :
        this(new PaginatedResult<ProductWithSalesInfoResponse>(dtoProducts.Map(p => new ProductWithSalesInfoResponse(p))))
    {
    }

    [JsonPropertyName("products")]
    public PaginatedResult<ProductWithSalesInfoResponse> Products { get; private set; } = products;
}