using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Admin.Products.Update;

public class ProductNotFoundException() : ResponseException("Product.NotFound", "Product was not found.");