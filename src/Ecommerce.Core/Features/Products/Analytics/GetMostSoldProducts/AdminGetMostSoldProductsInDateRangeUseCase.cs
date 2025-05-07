using Ecommerce.Core.Abstractions.Analytics.Models.Products;
using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;

internal class AdminGetMostSoldProductsInDateRangeUseCase(
    IAnalyticsProductsService analyticsProductsService,
    ApplicationDbContext dbContext
) : IAdminGetMostSoldProductsInDateRangeUseCase
{
    public async Task<PaginatedEnumerable<ProductWithSalesInfoDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default)
    {
        PaginatedEnumerable<ProductSalesDto> result =
            await analyticsProductsService.GetMostSoldProductsAsync(paginationQuery, dateRangeQuery, cancellationToken);

        HashSet<Guid> productIds = result.Data.Select(x => x.ProductId).ToHashSet();

        List<ProductDto> products = await dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Select(p => new ProductDto(p))
            .ToListAsync(cancellationToken);

        List<ProductWithSalesInfoDto> productsWithSalesInfo = new(products.Count);
        foreach (ProductDto product in products)
        {
            ProductSalesDto salesDto = result.Data.First(x => x.ProductId == product.Id);
            productsWithSalesInfo.Add(new ProductWithSalesInfoDto(product, salesDto.TotalSold));
        }

        return new PaginatedEnumerable<ProductWithSalesInfoDto>(
            productsWithSalesInfo,
            paginationQuery.PageSize,
            paginationQuery.PageNumber,
            result.TotalCount
        );
    }
}