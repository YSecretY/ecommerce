namespace Ecommerce.Core.Abstractions.Analytics.Models.Products;

public class ProductStatisticsDto(int totalSales, int totalViews)
{
    public int TotalSales { get; private set; } = totalSales;

    public int TotalViews { get; private set; } = totalViews;
}