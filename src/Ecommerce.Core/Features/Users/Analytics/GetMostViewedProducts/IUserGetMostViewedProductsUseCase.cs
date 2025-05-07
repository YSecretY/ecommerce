using Ecommerce.Core.Abstractions.Models.Products;

namespace Ecommerce.Core.Features.Users.Analytics.GetMostViewedProducts;

public interface IUserGetMostViewedProductsUseCase
{
    public Task<List<ProductDto>> HandleAsync(int count, CancellationToken cancellationToken = default);
}