using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Database;

internal class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllUsersDatabaseEntityTypeConfigurations();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;
}