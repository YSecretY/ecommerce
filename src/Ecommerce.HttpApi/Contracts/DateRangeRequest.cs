using System.Text.Json.Serialization;
using Ecommerce.Extensions.Types;

namespace Ecommerce.HttpApi.Contracts;

public class DateRangeRequest
{
    [JsonPropertyName("from")]
    public DateOnly From { get; set; }

    [JsonPropertyName("to")]
    public DateOnly To { get; set; }

    public DateRangeQuery ToDateRangeQuery() =>
        new(From, To);
}