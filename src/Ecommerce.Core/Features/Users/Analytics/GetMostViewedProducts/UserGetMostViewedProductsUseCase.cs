using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Users.Analytics.GetMostViewedProducts;

internal class UserGetMostViewedProductsUseCase(
    IAnalyticsUserService analyticsUserService,
    IIdentityUserAccessor identityUserAccessor,
    ApplicationDbContext dbContext
) : IUserGetMostViewedProductsUseCase
{
    private const int MaxProductsCount = 100;

    public async Task<List<ProductDto>> HandleAsync(int count, CancellationToken cancellationToken = default)
    {
        if (count > MaxProductsCount)
            throw new ResponseValidationException(
                $"Cannot get more than {MaxProductsCount} products. Reduce count parameter.");

        Guid userId = identityUserAccessor.GetUserId();

        List<Guid> productIds = await analyticsUserService.GetUserMostViewedProductsAsync(userId, count, cancellationToken);

        return await dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .Select(p => new ProductDto(p))
            .ToListAsync(cancellationToken);
    }
}