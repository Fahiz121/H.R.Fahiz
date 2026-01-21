using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Portfolios;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Portfolios.Commands;

internal sealed class DeletePortfolioCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<DeletePortfolioCommand>
{

    public async Task<Result> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        // 1️⃣ Find portfolio
        Portfolio portfolio = await context.Portfolios
            .Include(p => p.PortfolioImages) // Include images for cascade or future use
            .FirstOrDefaultAsync(p => p.Id == request.PortfolioId, cancellationToken) ?? throw new KeyNotFoundException($"Portfolio with Id {request.PortfolioId} not found.");

        // 2️⃣ Soft delete (preferred)
        // If you have IsActive / IsDeleted property, use that
        //_portfolio.IsActive = false;

        // 3️⃣ Hard delete (optional)
        context.Portfolios.Remove(portfolio);

        // 4️⃣ Save changes
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
