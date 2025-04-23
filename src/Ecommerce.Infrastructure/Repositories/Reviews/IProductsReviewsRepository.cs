using Ecommerce.Domain.Reviews;

namespace Ecommerce.Infrastructure.Repositories.Reviews;

public interface IProductsReviewsRepository
{
    public Task AddAsync(ProductReview productReview, CancellationToken cancellationToken = default);
}