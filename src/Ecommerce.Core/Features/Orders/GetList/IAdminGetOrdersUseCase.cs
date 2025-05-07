using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.Core.Features.Orders.GetList;

public interface IAdminGetOrdersUseCase
{
    public Task<PaginatedEnumerable<OrderDto>> HandleAsync(PaginationQuery paginationQuery, OrderStatus? status = null,
        CancellationToken cancellationToken = default);
}