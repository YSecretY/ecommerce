using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Exceptions.Reviews;

public class ProductReviewNotFoundException() : ResponseException("ProductReview.NotFound", "Product review was not found.");