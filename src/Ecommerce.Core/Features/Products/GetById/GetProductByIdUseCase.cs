using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Products.GetById;

public class GetProductByIdUseCase(
    ApplicationDbContext dbContext,
    IEventPublisher eventPublisher,
    IIdentityUserAccessor identityUserAccessor,
    IDateTimeProvider dateTimeProvider
) : IGetProductByIdUseCase
{
    public async Task<ProductDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Product product = await dbContext.Products
                              .AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken)
                          ?? throw new ProductNotFoundException();

        if (identityUserAccessor.IsAuthenticated() && !identityUserAccessor.IsInRole(UserRole.Admin))
        {
            Guid userId = identityUserAccessor.GetUserId();

            await eventPublisher.PublishAsync(
                new ProductViewedEvent(
                    productId: product.Id,
                    userId: userId,
                    occuredAtUtc: dateTimeProvider.UtcNow
                ),
                cancellationToken
            );
        }

        return new ProductDto(product);
    }
}