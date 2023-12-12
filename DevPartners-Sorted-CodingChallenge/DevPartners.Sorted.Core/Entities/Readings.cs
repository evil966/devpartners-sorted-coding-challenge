using System.Text.Json.Serialization;

namespace DevPartners.Sorted.Core.Entities;

public class Readings
{
    [JsonPropertyName("@context")]
    public string? Context { get; set; }
    public Meta? Meta { get; set; }
    public IEnumerable<Item>? Items { get; set; }
}

