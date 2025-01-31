using FluentValidation;
using RealState.Application.Features.Properties.AddImage.Commands;

namespace RealState.Application.Validators;

public class AddPropertyImageValidator : AbstractValidator<AddPropertyImageCommand>
{
    public AddPropertyImageValidator()
    {
        RuleFor(p => p.Image)
         .NotEmpty()
         .Must(IsValidBase64).WithMessage("Invalid Base64 format."); ;
    }

    private bool IsValidBase64(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64)) return false;

        try
        {
            Convert.FromBase64String(base64);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
