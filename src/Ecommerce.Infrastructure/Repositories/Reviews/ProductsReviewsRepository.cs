using Ecommerce.Domain.Reviews;
using Ecommerce.Extensions.Types;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories.Reviews;

internal class ProductsReviewsRepository(
    ProductsDbContext dbContext
) : IProductsReviewsRepository
{
    public async Task AddAsync(ProductReview productReview, CancellationToken cancellationToken = default) =>
        await dbContext.ProductsReviews.AddAsync(productReview, cancellationToken);

    public async Task<ProductReview?> GetByIdAsync(Guid id, bool tracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProductReview> query = dbContext.ProductsReviews;

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<PaginatedEnumerable<ProductReview>> GetListAsync(PaginationQuery paginationQuery,
        bool tracking = false,
        Func<IQueryable<ProductReview>, IQueryable<ProductReview>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ProductReview> query = dbContext.ProductsReviews.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        if (filter is not null)
            query = filter(query);

        int totalCount = await query.CountAsync(cancellationToken);

        List<ProductReview> products = await query
            .OrderByDescending(p => p.CreatedAtUtc)
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
            .Take(paginationQuery.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedEnumerable<ProductReview>(products, paginationQuery.PageSize, paginationQuery.PageNumber,
            totalCount);
    }

    public void SoftDelete(ProductReview productReview) =>
        productReview.Delete();
}