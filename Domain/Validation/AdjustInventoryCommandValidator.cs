using Core.Commands;
using FluentValidation;

namespace Core.Validation;

internal class AdjustInventoryCommandValidator : AbstractValidator<AdjustInventoryCommand>
{
    public AdjustInventoryCommandValidator()
    {
        RuleFor(x => x.Sku)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Reason)
            .NotNull()
            .NotEmpty();
    }
}