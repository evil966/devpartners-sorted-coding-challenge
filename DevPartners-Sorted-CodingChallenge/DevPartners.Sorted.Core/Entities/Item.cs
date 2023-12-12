using System.Text.Json.Serialization;

namespace DevPartners.Sorted.Core.Entities;

public class Item
{
    [JsonPropertyName("@id")]
    public string? Id { get; set; }

    public string? DateTime { get; set; }

    public string? Measure { get; set; }

    public double Value { get; set; }
}
