using System.Text.Json.Serialization;
using Ecommerce.Extensions.Types;

namespace Ecommerce.HttpApi.Contracts;

public class PaginationRequest
{
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }

    public PaginationQuery ToPaginationQuery() =>
        new(PageSize, PageNumber);
}