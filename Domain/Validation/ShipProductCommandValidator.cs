using Core.Commands;
using FluentValidation;

namespace Core.Validation;

internal class ShipProductCommandValidator : AbstractValidator<ShipProductCommand>
{
    public ShipProductCommandValidator()
    {
        RuleFor(x => x.Sku)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}