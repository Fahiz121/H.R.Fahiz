namespace Application.Portfolios.Queries;

public sealed class PortfolioResponse
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsFeatured { get; set; }
    public List<string> ImagePaths { get; set; } = new();
}
