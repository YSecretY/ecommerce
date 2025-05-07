using Ecommerce.Core.Abstractions.Analytics.Models.Products;
using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;

internal class AdminGetMostViewedProductsInDateRangeUseCase(
    IAnalyticsProductsService analyticsProductsService,
    ApplicationDbContext dbContext
) : IAdminGetMostViewedProductsInDateRangeUseCase
{
    public async Task<PaginatedEnumerable<ProductWithViewsInfoDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default)
    {
        PaginatedEnumerable<ProductViewsDto> result =
            await analyticsProductsService.GetMostViewedProductsAsync(paginationQuery, dateRangeQuery, cancellationToken);

        HashSet<Guid> productIds = result.Data.Select(x => x.ProductId).ToHashSet();

        List<ProductDto> products = await dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Select(p => new ProductDto(p))
            .ToListAsync(cancellationToken);

        List<ProductWithViewsInfoDto> productsWithViewsInfo = new(products.Count);
        foreach (ProductDto product in products)
        {
            ProductViewsDto viewsDto = result.Data.First(x => x.ProductId == product.Id);
            productsWithViewsInfo.Add(new ProductWithViewsInfoDto(product, viewsDto.ViewsCount));
        }

        return new PaginatedEnumerable<ProductWithViewsInfoDto>(
            productsWithViewsInfo,
            paginationQuery.PageSize,
            paginationQuery.PageNumber,
            result.TotalCount
        );
    }
}