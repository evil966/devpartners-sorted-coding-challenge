using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Models;

public class ApiCallResult : ApiCallBaseResult
{
    public Readings? Readings { get; set; }
}
