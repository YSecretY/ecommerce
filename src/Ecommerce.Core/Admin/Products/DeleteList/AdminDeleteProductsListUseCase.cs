using Ecommerce.Domain.Products;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Admin.Products.DeleteList;

public class AdminDeleteProductsListUseCase(
    IProductsRepository productsRepository,
    IProductsUnitOfWork unitOfWork
) : IAdminDeleteProductsListUseCase
{
    private const int BatchSize = 300;

    public async Task HandleAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        foreach (Guid[] currentIds in ids.Chunk(BatchSize))
        {
            List<Product> products = await productsRepository.GetListAsync(currentIds, true, cancellationToken);

            productsRepository.SoftDelete(products);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}