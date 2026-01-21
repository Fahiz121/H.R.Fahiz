using Application.Abstractions.Messaging;

namespace Application.Portfolios.Commands;

public sealed record DeletePortfolioCommand(Guid PortfolioId) : ICommand;
