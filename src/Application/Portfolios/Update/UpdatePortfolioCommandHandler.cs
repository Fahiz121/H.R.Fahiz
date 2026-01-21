using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Portfolios;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Portfolios.Commands;

internal sealed class UpdatePortfolioCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<UpdatePortfolioCommand>
{

    public async Task<Result> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        // 1️⃣ Fetch portfolio
        Portfolio portfolio = await context.Portfolios
            .Include(p => p.PortfolioImages)
            .Where(p => p.IsFeatured) // Soft delete
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken) ?? throw new KeyNotFoundException($"Portfolio with Id {request.Id} not found.");

        // 2️⃣ Update fields
        // Using encapsulated methods is better if domain logic exists
        portfolio.UpdateDetails(
            request.Category,
            request.SubCategory,
            request.Title,
            request.Description,
            request.IsFeatured
        );

        // 3️⃣ Replace images
        // Clear old images
        portfolio.ClearImages();

        // Add new images
        foreach (string path in request.ImagePaths)
        {
            portfolio.AddImage(path);
        }

        // 4️⃣ Save changes
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
