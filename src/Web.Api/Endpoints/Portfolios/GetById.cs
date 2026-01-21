using Application.Abstractions.Messaging;
using Application.Portfolios.Queries;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Portfolios;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("portfolios/{portfolioId}", async (
            bool IsFeatured,
            Guid portfolioId,
            IQueryHandler<GetPortfolioByIdQuery, PortfolioResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPortfolioByIdQuery(portfolioId, IsFeatured);
            Result<PortfolioResponse> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Portfolios);
    }
}
