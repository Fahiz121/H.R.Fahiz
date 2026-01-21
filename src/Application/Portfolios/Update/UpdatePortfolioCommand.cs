using Application.Abstractions.Messaging;

namespace Application.Portfolios.Commands;

public sealed record UpdatePortfolioCommand(
    Guid Id,
    string Category,
    string SubCategory,
    string Title,
    string Description,
    bool IsFeatured,
    List<string> ImagePaths // Replace all images
) : ICommand;
