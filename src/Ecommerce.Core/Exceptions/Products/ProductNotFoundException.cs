using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Exceptions.Products;

public class ProductNotFoundException(object? additionalData = null)
    : ResponseException("Product.NotFound", "Product was not found.", additionalData);