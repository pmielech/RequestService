namespace RequestService.Data;

public class HtmlElementType
{
    public string? Name { get; set; }
    public string? Xpath { get; set; }
    // public Func<string, decimal>? Parse { get; set; }
    public decimal Value { get; set; } = 0;
}