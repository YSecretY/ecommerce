using System.ComponentModel.DataAnnotations;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Domain.Products;

namespace Ecommerce.Persistence.Domain.Orders.Validators;

public class OrderValidator
{
    public static void Validate(Order order)
    {
        List<ValidationError> errors = [];

        AddressValidator.Validate(order.Address);

        if (order.OrderItems.Count is 0)
            errors.Add(new ValidationError("Order items cannot be empty."));

        foreach (OrderItem item in order.OrderItems)
            if (item.Quantity <= 0)
                errors.Add(new ValidationError($"Order item quantity cannot be negative or 0. Item:{item.Id}"));

        if (string.IsNullOrWhiteSpace(order.CurrencyCode))
            throw new ValidationException("Order currency code cannot be empty.");

        if (order.CurrencyCode.Length > ProductValidator.MaxCurrencyCodeLength)
            throw new ValidationException($"CurrencyCode cannot exceed {ProductValidator.MaxCurrencyCodeLength}.");

        ResponseValidationException.ThrowIf(errors.Any, errors);
    }

    public static Order CreateOrThrow(
        Guid userId,
        List<OrderItem> items,
        string currencyCode,
        Address address,
        OrderStatus status,
        DateTime createdAtUtc,
        DateTime updatedAtUtc
    )
    {
        Order order = new(
            userId: userId,
            items: items,
            currencyCode: currencyCode,
            address: address,
            status: status,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc
        );

        Validate(order);

        return order;
    }
}