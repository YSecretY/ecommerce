using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts.Products;

namespace Ecommerce.HttpApi.Contracts.Admin.Products.GetMostViewedInDateRange;

public class AdminGetMostViewedProductsInDateRangeResponse(PaginatedResult<ProductWithViewsInfoResponse> products)
{
    public AdminGetMostViewedProductsInDateRangeResponse(PaginatedEnumerable<ProductWithViewsInfoDto> dtoProducts) :
        this(new PaginatedResult<ProductWithViewsInfoResponse>(dtoProducts.Map(p => new ProductWithViewsInfoResponse(p))))
    {
    }

    [JsonPropertyName("products")]
    public PaginatedResult<ProductWithViewsInfoResponse> Products { get; private set; } = products;
}