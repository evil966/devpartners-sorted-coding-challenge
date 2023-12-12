namespace DevPartners.Sorted.Application.Models;

public class RainfallReadingResponse
{
    public IEnumerable<RainfallReading> Readings { get; set; } = default!;
}
