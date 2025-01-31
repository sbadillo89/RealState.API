using FluentValidation;
using RealState.Application.Features.Properties.Create.Commands;

namespace RealState.Application.Validators;

public class CreatePropertyValidator : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyValidator()
    {
        RuleFor(p => p.Name)
        .NotEmpty()
        .MaximumLength(100);

        RuleFor(p => p.Address)
        .NotEmpty()
        .MaximumLength(250);

        RuleFor(p => p.InternalCode)
          .NotEmpty()
          .MaximumLength(50);

        RuleFor(p => p.Year)
         .GreaterThan(0);

        RuleFor(p => p.Price)
            .GreaterThan(0);
    }
}
