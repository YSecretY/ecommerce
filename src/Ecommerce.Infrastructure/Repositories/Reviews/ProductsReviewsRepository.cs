using Ecommerce.Domain.Reviews;
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

    public void Remove(ProductReview productReview) =>
        dbContext.ProductsReviews.Remove(productReview);
}