using Application.Abstractions.Messaging;

namespace Application.Portfolios.Queries;

public sealed record GetPortfolioByIdQuery(Guid PortfolioId, bool IsFeatured) : IQuery<PortfolioResponse>;
