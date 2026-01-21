using FluentValidation;

namespace Application.Portfolios.Commands;

public sealed class CreatePortfolioCommandValidator
    : AbstractValidator<CreatePortfolioCommand>
{
    public CreatePortfolioCommandValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SubCategory)
            .MaximumLength(100);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleForEach(x => x.ImagePaths)
            .NotEmpty()
            .WithMessage("Image path cannot be empty.");
    }
}
