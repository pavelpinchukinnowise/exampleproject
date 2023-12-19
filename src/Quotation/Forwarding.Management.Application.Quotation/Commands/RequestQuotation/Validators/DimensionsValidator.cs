using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Resources;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;

public class DimensionsValidator : AbstractValidator<Dimensions>
{
    public DimensionsValidator()
    {
        RuleFor(x => x.Height)
            .NotEmpty()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required, nameof(Dimensions.Height)))
            .GreaterThan(0);

        RuleFor(x => x.Length)
            .NotEmpty()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required, nameof(Dimensions.Length)))
            .GreaterThan(0);

        RuleFor(x => x.Width)
            .NotEmpty()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required, nameof(Dimensions.Width)))
            .GreaterThan(0);

        RuleFor(x => x.Unit)
            .NotNull()
            .WithMessage(string.Format(CultureInfo.CurrentCulture, Messages.Required, nameof(Dimensions.Unit)))
            .IsInEnum();
    }
}
