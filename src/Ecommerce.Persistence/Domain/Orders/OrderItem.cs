using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Domain.Orders;

public class OrderItem(
    Guid productId,
    Guid orderId,
    int quantity,
    decimal pricePerUnit,
    string currencyCode
)
{
    private const string TableName = "OrderItems";

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid ProductId { get; private set; } = productId;

    public Product Product { get; private set; } = null!;

    public Guid OrderId { get; private set; } = orderId;

    public Order Order { get; private set; } = null!;

    public int Quantity { get; private set; } = quantity;

    public decimal PricePerUnit { get; private set; } = pricePerUnit;

    [MaxLength(ProductValidator.MaxCurrencyCodeLength)]
    public string CurrencyCode { get; private set; } = currencyCode;

    public decimal TotalPrice => PricePerUnit * Quantity;

    public static void Builder(EntityTypeBuilder<OrderItem> orderItem)
    {
        orderItem.ToTable(TableName);

        orderItem.HasKey(oi => oi.Id);

        orderItem.Property(oi => oi.Id)
            .ValueGeneratedNever();

        orderItem.Property(oi => oi.ProductId)
            .IsRequired();

        orderItem.Property(oi => oi.OrderId)
            .IsRequired();

        orderItem.Property(oi => oi.Quantity)
            .IsRequired();

        orderItem.Property(oi => oi.PricePerUnit)
            .IsRequired();

        orderItem.Property(oi => oi.CurrencyCode)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxCurrencyCodeLength);

        orderItem.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        orderItem.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}