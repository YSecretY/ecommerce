using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Infrastructure.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Orders;
using Ecommerce.Persistence.Domain.Orders.Validators;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Orders.Create;

public class UserCreateOrderUseCase(
    ApplicationDbContext dbContext,
    IIdentityUserAccessor identityUserAccessor,
    IDateTimeProvider dateTimeProvider
) : IUserCreateOrderUseCase
{
    public async Task<Guid> HandleAsync(UserCreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        DateTime utcNow = dateTimeProvider.UtcNow;

        Order order = new(
            userId,
            [],
            command.CurrencyCode,
            command.Address.ToEntity(),
            OrderStatus.Pending,
            utcNow,
            utcNow
        );

        HashSet<Guid> productsIds = command.OrderItems.Select(i => i.ProductId).ToHashSet();

        List<Product> products = await dbContext.Products
            .Where(p => productsIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        if (products.Count != command.OrderItems.Count)
        {
            List<Guid> missingIds = productsIds
                .Where(id => products.All(p => p.Id != id))
                .ToList();

            throw new ProductNotFoundException(
                $"Not all products were found to complete this order. Invalid ids are: {string.Join(", ", missingIds)}");
        }

        foreach (Product product in products)
        {
            if (!product.IsInStock)
                throw new ProductOutOfStockException(product.Id);

            OrderItemCommandModel item = command.OrderItems.First(i => i.ProductId == product.Id);

            if (item.Quantity > product.TotalCount)
                throw new ProductNotEnoughException(product.Id);

            product.Buy(item.Quantity);

            order.OrderItems.Add(
                new OrderItem(
                    product.Id,
                    order.Id,
                    item.Quantity,
                    product.DisplayPrice(utcNow),
                    command.CurrencyCode)
            );
        }

        OrderValidator.Validate(order);

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}