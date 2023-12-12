using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DevPartners.Sorted.Application.Models;

public class ReadingsResponse
{
    [JsonPropertyName("@context")]
    public string? Context { get; set; }
}
