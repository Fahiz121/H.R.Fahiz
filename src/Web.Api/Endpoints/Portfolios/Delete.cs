using Application.Abstractions.Messaging;
using Application.Portfolios.Commands;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Portfolios;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("portfolios/{portfolioId}", async (
            Guid portfolioId,
            ICommandHandler<DeletePortfolioCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeletePortfolioCommand(portfolioId);
            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(
               () => Results.Ok(),
               error => CustomResults.Problem(result)
               );
        })
        .WithTags(Tags.Portfolios);
    }
}
