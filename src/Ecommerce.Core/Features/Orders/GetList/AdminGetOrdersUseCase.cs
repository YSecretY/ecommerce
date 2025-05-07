using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Orders;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Orders.GetList;

internal class AdminGetOrdersUseCase(ApplicationDbContext dbContext) : IAdminGetOrdersUseCase
{
    public async Task<PaginatedEnumerable<OrderDto>> HandleAsync(PaginationQuery paginationQuery, OrderStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Order> ordersQuery = dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Product);

        if (status is not null)
            ordersQuery = ordersQuery.Where(x => x.Status == status);

        return await ordersQuery
            .Select(o => new OrderDto(o))
            .ToPaginatedEnumerableAsync(paginationQuery, cancellationToken);
    }
}