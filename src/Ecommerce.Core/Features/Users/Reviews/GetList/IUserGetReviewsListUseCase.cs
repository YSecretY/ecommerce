using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Users.Reviews.GetList;

public interface IUserGetReviewsListUseCase
{
    public Task<PaginatedEnumerable<ProductReviewDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default);
}