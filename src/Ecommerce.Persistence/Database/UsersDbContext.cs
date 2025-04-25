using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Database;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllUsersDatabaseEntityTypeConfigurations();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; } = null!;
}