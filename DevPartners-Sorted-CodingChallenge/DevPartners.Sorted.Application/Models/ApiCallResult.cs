using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Models;

public class ApiCallResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public Readings? Readings { get; set; }
}
