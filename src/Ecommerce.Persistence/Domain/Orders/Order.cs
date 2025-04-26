using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Orders.Validators;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Domain.Orders;

public class Order
{
    /// <summary>
    /// Constructor for Entity Framework
    /// </summary>
#pragma warning disable CS8618, CS9264
    protected Order()
#pragma warning restore CS8618, CS9264
    {
    }

    public Order(
        Guid userId,
        List<OrderItem> items,
        string currencyCode,
        Address address,
        OrderStatus status,
        DateTime createdAtUtc,
        DateTime updatedAtUtc
    )
    {
        UserId = userId;
        OrderItems = items;
        CurrencyCode = currencyCode;
        Address = address;
        Status = status;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    private const string TableName = "Orders";

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    public ICollection<OrderItem> OrderItems { get; init; }

    public Address Address { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime UpdatedAtUtc { get; private set; }

    public DateTime? DeliveredAt { get; private set; }

    public decimal TotalPrice => OrderItems.Sum(i => i.TotalPrice);

    [MaxLength(ProductValidator.MaxCurrencyCodeLength)]
    public string CurrencyCode { get; private set; }

    public void Deliver(DateTime utcNow) =>
        DeliveredAt = utcNow;

    public static void Builder(EntityTypeBuilder<Order> order)
    {
        order.ToTable(TableName);

        order.HasKey(o => o.Id);

        order.Property(o => o.Id)
            .ValueGeneratedNever();

        order.Property(o => o.UserId)
            .IsRequired();

        order.Property(o => o.CreatedAtUtc)
            .IsRequired();

        order.Property(o => o.UpdatedAtUtc)
            .IsRequired();

        order.Property(o => o.DeliveredAt)
            .IsRequired(false);

        order.Property(o => o.CurrencyCode)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxCurrencyCodeLength);

        order.Property(o => o.Status)
            .IsRequired();

        order.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        order.HasMany(o => o.OrderItems)
            .WithOne(i => i.Order)
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        order.OwnsOne(o => o.Address, address =>
        {
            address.Property(a => a.Street).HasMaxLength(AddressValidator.MaxStreetLength);
            address.Property(a => a.City).HasMaxLength(AddressValidator.MaxCityLength);
            address.Property(a => a.PostalCode).HasMaxLength(AddressValidator.MaxPostalCodeLength);

            address.WithOwner();
        });
    }
}