using Application.Abstractions.Messaging;

namespace Application.Portfolios.Commands;

public sealed record CreatePortfolioCommand(
    string Category,
    string SubCategory,
    string Title,
    string Description,
    bool IsFeatured,
    List<string> ImagePaths
) : ICommand<Guid>; // Returns the created Portfolio Id
