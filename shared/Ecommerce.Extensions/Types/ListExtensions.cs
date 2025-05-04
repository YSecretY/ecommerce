namespace Ecommerce.Extensions.Types;

public static class ListExtensions
{
    public static bool IsNotEmpty<T>(this IList<T> enumerable) =>
        enumerable.Count > 0;
}