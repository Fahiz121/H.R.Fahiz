using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Portfolios;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Portfolios.Commands;

internal sealed class CreatePortfolioCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreatePortfolioCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        // 1️⃣ Create Portfolio Aggregate
        var portfolio = new Portfolio(
            request.Category,
            request.SubCategory,
            request.Title,
            request.Description,
            request.IsFeatured
        );

        // 2️⃣ Add Portfolio Images
        foreach (string path in request.ImagePaths)
        {
            portfolio.AddImage(path);
        }

        // 3️⃣ Save to DB
        context.Portfolios.Add(portfolio);
        await context.SaveChangesAsync(cancellationToken);

        // 4️⃣ Return Id
        return portfolio.Id;
    }
}
