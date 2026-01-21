using Application.Abstractions.Messaging;
using Application.Portfolios.Commands;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Portfolios;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("portfolios/{portfolioId}", async (
            Guid portfolioId,
            UpdatePortfolioCommand command,
            ICommandHandler<UpdatePortfolioCommand> handler,
            CancellationToken cancellationToken) =>
        {
            // Ensure the command has correct Id
            if (command.Id != portfolioId)
            {
                return CustomResults.Problem("Portfolio Id mismatch");
            }

            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(
               () => Results.Ok(),
               error => CustomResults.Problem(result)
               );
        })
        .WithTags(Tags.Portfolios);
    }
}
