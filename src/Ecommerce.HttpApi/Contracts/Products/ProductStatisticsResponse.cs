using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Analytics.Models.Products;

namespace Ecommerce.HttpApi.Contracts.Products;

public class ProductStatisticsResponse(int totalSales, int totalViews)
{
    [JsonPropertyName("totalSales")]
    public int TotalSales { get; private set; } = totalSales;

    [JsonPropertyName("totalViews")]
    public int TotalViews { get; private set; } = totalViews;

    public ProductStatisticsResponse(ProductStatisticsDto dto)
        : this(dto.TotalSales, dto.TotalViews)
    {
    }
}