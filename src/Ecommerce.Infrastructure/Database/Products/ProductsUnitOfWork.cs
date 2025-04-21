namespace Ecommerce.Infrastructure.Database.Products;

internal class ProductsUnitOfWork(
    ProductsDbContext dbContext
) : IProductsUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}