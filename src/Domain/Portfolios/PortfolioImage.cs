using SharedKernel;

namespace Domain.Portfolio;

public sealed class PortfolioImage : Entity
{
    public Guid Id { get; set; }
    public string ImagePath { get; private set; }

    private PortfolioImage() { } // EF Core

    internal PortfolioImage(string imagePath)
    {
        ImagePath = imagePath;
    }
}
