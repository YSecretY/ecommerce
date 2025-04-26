using Ecommerce.Persistence.Domain.Orders;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Reviews;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Database;

public static class EntityConfigurations
{
    public static void ApplyAllEntityTypeConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(User.Builder);
        modelBuilder.Entity<Product>(Product.Builder);
        modelBuilder.Entity<ProductReview>(ProductReview.Builder);
        modelBuilder.Entity<ProductReviewReply>(ProductReviewReply.Builder);
        modelBuilder.Entity<Order>(Order.Builder);
        modelBuilder.Entity<OrderItem>(OrderItem.Builder);
    }
}