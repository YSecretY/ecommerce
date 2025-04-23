using System.Text.Json.Serialization;

namespace Ecommerce.HttpApi.Contracts.Admin.Products;

public class AdminDeleteProductsListRequest
{
    [JsonPropertyName("productsIds")]
    public List<Guid> ProductsIds { get; set; } = null!;
}