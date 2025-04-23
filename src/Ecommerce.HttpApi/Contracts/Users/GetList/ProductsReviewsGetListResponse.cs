using System.Text.Json.Serialization;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts.Users.Reviews;

namespace Ecommerce.HttpApi.Contracts.Users.GetList;

public class ProductsReviewsGetListResponse(PaginatedResult<ProductReviewResponse> reviews)
{
    public ProductsReviewsGetListResponse(PaginatedEnumerable<ProductReviewResponse> enumerable)
        : this(new PaginatedResult<ProductReviewResponse>(enumerable))
    {
    }

    [JsonPropertyName("reviews")]
    public PaginatedResult<ProductReviewResponse> Reviews { get; private set; } = reviews;
}