namespace Ecommerce.Persistence.Domain;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }

    public void SoftDelete();
}