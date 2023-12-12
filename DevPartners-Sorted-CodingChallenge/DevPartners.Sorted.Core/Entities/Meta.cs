namespace DevPartners.Sorted.Core.Entities;

public  class Meta
{
    public string? Publisher { get; set; }
    public string? License { get; set; }
    public string? Documentation { get; set; }
    public string? Version { get; set; }
    public int Limit { get; set; } = 500;
    public List<string>? HasFormat { get; set; }
}
