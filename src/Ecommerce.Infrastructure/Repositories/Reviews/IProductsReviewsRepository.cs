using Ecommerce.Domain.Reviews;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Infrastructure.Repositories.Reviews;

public interface IProductsReviewsRepository
{
    public Task AddAsync(ProductReview productReview, CancellationToken cancellationToken = default);

    public Task<ProductReview?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);

    public Task<PaginatedEnumerable<ProductReview>> GetListAsync(
        PaginationQuery paginationQuery,
        bool tracking = false,
        Func<IQueryable<ProductReview>, IQueryable<ProductReview>>? filter = null,
        CancellationToken cancellationToken = default);

    public void Remove(ProductReview productReview);
}