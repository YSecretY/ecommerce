using Ecommerce.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Extensions;

public static class DbContextExtensions
{
    public static void SoftDelete<TEntity>(this DbContext _, TEntity entity)
        where TEntity : class, ISoftDeletable => entity.SoftDelete();

    public static void SoftDeleteRange<TEntity>(this DbContext dbContext, IEnumerable<TEntity> entities)
        where TEntity : class, ISoftDeletable
    {
        foreach (TEntity entity in entities)
            dbContext.SoftDelete(entity);
    }
}