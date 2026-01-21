using Application.Abstractions.Messaging;
using Application.Portfolios.Queries;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Portfolios;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("portfolios", async (
            bool isFeatured,
            IQueryHandler<GetPortfoliosQuery, List<PortfolioResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetPortfoliosQuery(isFeatured);
            Result<List<PortfolioResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Portfolios);
    }
}
