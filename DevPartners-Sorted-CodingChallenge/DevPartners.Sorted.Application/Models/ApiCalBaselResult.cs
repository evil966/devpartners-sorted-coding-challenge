using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Models;

public class ApiCallBaseResult
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}
