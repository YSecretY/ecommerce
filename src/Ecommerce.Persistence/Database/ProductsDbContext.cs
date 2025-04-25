using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Database;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllProductsDatabaseEntityTypeConfigurations();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductReview> ProductsReviews { get; set; } = null!;

    public DbSet<ProductReviewReply> ProductReviewReplies { get; set; } = null!;
}