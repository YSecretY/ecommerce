namespace Ecommerce.Infrastructure.Database.Products;

public interface IProductsUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}