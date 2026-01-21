using Application.Abstractions.Messaging;
using Application.Portfolios.Commands;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Portfolios;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("portfolios", async (
            CreatePortfolioCommand command,
            ICommandHandler<CreatePortfolioCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Created, CustomResults.Problem);
        })
        .WithTags(Tags.Portfolios);
    }
}
