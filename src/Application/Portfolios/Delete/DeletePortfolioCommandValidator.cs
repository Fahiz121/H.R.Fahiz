using FluentValidation;

namespace Application.Portfolios.Commands;

public sealed class DeletePortfolioCommandValidator
    : AbstractValidator<DeletePortfolioCommand>
{
    public DeletePortfolioCommandValidator()
    {
        RuleFor(x => x.PortfolioId)
            .NotEmpty()
            .WithMessage("Portfolio Id is required.");
    }
}
