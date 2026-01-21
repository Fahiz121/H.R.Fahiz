using Application.Abstractions.Messaging;

namespace Application.Portfolios.Queries;

public sealed record GetPortfoliosQuery(bool IsFeatured) : IQuery<List<PortfolioResponse>>;
