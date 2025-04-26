using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Reviews;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllEntityTypeConfigurations();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<ProductReview> ProductsReviews { get; set; } = null!;

    public DbSet<ProductReviewReply> ProductReviewReplies { get; set; } = null!;
}