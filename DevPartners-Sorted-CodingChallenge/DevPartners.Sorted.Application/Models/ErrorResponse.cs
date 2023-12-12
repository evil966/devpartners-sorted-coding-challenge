using DevPartners.Sorted.Application.Models.Errors;

namespace DevPartners.Sorted.Application.Models;

public class ErrorResponse
{
    public string Message { get; set; } = default!;

    public IEnumerable<ErrorDetail> Detail { get; set; } = new List<ErrorDetail>();
}
