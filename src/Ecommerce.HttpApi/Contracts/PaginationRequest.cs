using System.Text.Json.Serialization;

namespace Ecommerce.HttpApi.Contracts;

public class PaginationRequest
{
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }
}