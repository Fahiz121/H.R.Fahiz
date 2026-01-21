using Domain.Portfolio;
using SharedKernel;

namespace Domain.Portfolios;

public sealed class Portfolio : Entity
{
    public Guid Id { get; set; }
    public string Category { get; private set; }
    public string SubCategory { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsFeatured { get; private set; }

    public ICollection<PortfolioImage> PortfolioImages { get; private set; }

    private Portfolio() { } // EF Core

    public Portfolio(
        string category,
        string subCategory,
        string title,
        string description,
        bool isFeatured = false)
    {
        Category = category;
        SubCategory = subCategory;
        Title = title;
        Description = description;
        IsFeatured = isFeatured;

        PortfolioImages = new List<PortfolioImage>();
    }

    public void AddImage(string imagePath)
    {
        PortfolioImages.Add(new PortfolioImage(imagePath));
    }

    public void ClearImages()
    {
        PortfolioImages.Clear();
    }

    public void UpdateDetails(string category, string subCategory, string title, string description, bool isFeatured)
    {
        Category = category;
        SubCategory = subCategory;
        Title = title;
        Description = description;
        IsFeatured = isFeatured;
    }
}
