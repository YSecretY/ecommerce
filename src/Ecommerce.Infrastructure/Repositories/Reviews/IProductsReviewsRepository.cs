using Ecommerce.Domain.Reviews;

namespace Ecommerce.Infrastructure.Repositories.Reviews;

public interface IProductsReviewsRepository
{
    public Task AddAsync(ProductReview productReview, CancellationToken cancellationToken = default);

    public Task<ProductReview?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);

    public void Remove(ProductReview productReview);
}