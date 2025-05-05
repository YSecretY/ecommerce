using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Exceptions.Products;

public class ProductOutOfStockException(Guid productId)
    : ResponseException("Product.OutOfStock", "Product is out of stock.", productId);