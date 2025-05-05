using System.Text.Json.Serialization;
using Ecommerce.Extensions.Types;

namespace Ecommerce.HttpApi.Contracts.Reviews.GetList;

public class ProductsReviewsGetListResponse(PaginatedResult<ProductReviewResponse> reviews)
{
    public ProductsReviewsGetListResponse(PaginatedEnumerable<ProductReviewResponse> enumerable)
        : this(new PaginatedResult<ProductReviewResponse>(enumerable))
    {
    }

    [JsonPropertyName("reviews")]
    public PaginatedResult<ProductReviewResponse> Reviews { get; private set; } = reviews;
}