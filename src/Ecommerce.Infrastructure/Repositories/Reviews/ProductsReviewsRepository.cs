using Ecommerce.Domain.Reviews;
using Ecommerce.Infrastructure.Database;

namespace Ecommerce.Infrastructure.Repositories.Reviews;

internal class ProductsReviewsRepository(
    ProductsDbContext dbContext
) : IProductsReviewsRepository
{
    public async Task AddAsync(ProductReview productReview, CancellationToken cancellationToken = default) =>
        await dbContext.ProductsReviews.AddAsync(productReview, cancellationToken);
}