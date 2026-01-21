using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Portfolios.Queries;
using Domain.Portfolios;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Portfolios.Queries;

internal sealed class GetPortfoliosQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetPortfoliosQuery, List<PortfolioResponse>>
{

    public async Task<Result<List<PortfolioResponse>>> Handle(GetPortfoliosQuery request, CancellationToken cancellationToken)
    {
        List<Portfolio> portfolios = await context.Portfolios
            .Include(p => p.PortfolioImages) // Include images
            .Where(p => p.IsFeatured) // Uncomment if using soft delete
            .OrderByDescending(p => p.IsFeatured)
            .ThenBy(p => p.Title)
            .ToListAsync(cancellationToken);

        // Projection to DTO
        var response = portfolios.Select(p => new PortfolioResponse
        {
            Id = p.Id,
            Category = p.Category,
            SubCategory = p.SubCategory,
            Title = p.Title,
            Description = p.Description,
            IsFeatured = p.IsFeatured,
            ImagePaths = p.PortfolioImages.Select(pi => pi.ImagePath).ToList()
        }).ToList();

        return response;
    }
}
