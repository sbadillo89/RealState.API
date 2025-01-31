using FluentValidation;
using RealState.Application.Features.Properties.ChangePrice.Commands;

namespace RealState.Application.Validators;

public class ChangePricePropertyValidator : AbstractValidator<ChangePriceCommand>
{
    public ChangePricePropertyValidator()
    {
        RuleFor(p => p.Price)
            .GreaterThan(0);
    }
}
