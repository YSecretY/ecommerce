using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Exceptions.Products;

public class ProductNotEnoughException(Guid productId)
    : ResponseException("Product.NotEnough", "Product quantity is too small.", productId);