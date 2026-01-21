using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Portfolios.Queries;
using Domain.Portfolios;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Portfolios.Queries;

internal sealed class GetPortfolioByIdQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetPortfolioByIdQuery, PortfolioResponse>
{

    public async Task<Result<PortfolioResponse>> Handle(GetPortfolioByIdQuery request, CancellationToken cancellationToken)
    {
        Portfolio? portfolio = await context.Portfolios
            .Include(p => p.PortfolioImages)
            .Where(p => p.IsFeatured) // Uncomment if soft delete
            .FirstOrDefaultAsync(p => p.Id == request.PortfolioId, cancellationToken);

        if (portfolio == null)
        {
            return null; // অথবা throw new KeyNotFoundException(...)
        }

        var response = new PortfolioResponse
        {
            Id = portfolio.Id,
            Category = portfolio.Category,
            SubCategory = portfolio.SubCategory,
            Title = portfolio.Title,
            Description = portfolio.Description,
            IsFeatured = portfolio.IsFeatured,
            ImagePaths = portfolio.PortfolioImages.Select(pi => pi.ImagePath).ToList()
        };

        return response;
    }
}
