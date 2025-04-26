using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Extensions.Types;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ResponseValidationException($"Invalid enum value: {value}");

        if (Enum.TryParse(value, ignoreCase, out TEnum result))
            return result;

        throw new ResponseValidationException($"Invalid enum value: {value}");
    }
}