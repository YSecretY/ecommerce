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
    public async Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default)
    {
        PaginatedEnumerable<Guid> result =
            await analyticsProductsService.GetMostSoldProductsAsync(paginationQuery, dateRangeQuery, cancellationToken);

        List<ProductDto> products = await dbContext.Products
            .Where(p => result.Data.Contains(p.Id))
            .Select(p => new ProductDto(p))
            .ToListAsync(cancellationToken);

        return new PaginatedEnumerable<ProductDto>(
            products,
            paginationQuery.PageSize,
            paginationQuery.PageNumber,
            result.TotalCount
        );
    }
}