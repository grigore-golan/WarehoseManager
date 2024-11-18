using Core.Commands;
using FluentValidation;

namespace Core.Validation;

internal class ReceiveProductCommandValidator : AbstractValidator<ReceiveProductCommand>
{
    public ReceiveProductCommandValidator()
    {
        RuleFor(x => x.Sku)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}