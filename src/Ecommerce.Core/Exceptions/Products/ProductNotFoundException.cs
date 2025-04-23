using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Exceptions.Products;

public class ProductNotFoundException() : ResponseException("Product.NotFound", "Product was not found.");