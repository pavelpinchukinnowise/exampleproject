using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Common.FluentValidation;
using Forwarding.Management.Application.Resources;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;

public class CargoSpecificationsValidator : AbstractValidator<CargoSpecifications>
{
    public CargoSpecificationsValidator()
    {
        RuleFor(x => x)
            .Must(x => (x.Volume is null && x.VolumeUnit is null && x.Dimensions is null)
                       || (x.Volume is not null && x.VolumeUnit is not null && x.Dimensions is null)
                       || (x.Volume is null && x.VolumeUnit is null && x.Dimensions is not null))
            .WithMessage(Messages.BothDimensionsAndVolumePresent);

        When(x => x.Volume is not null || x.VolumeUnit is not null,
            () =>
            {
                RuleFor(x => x.Volume)
                    .NotEmpty()
                    .WithMessage(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Messages.Required,
                            nameof(CargoSpecifications.Volume)))
                    .GreaterThan(0);

                RuleFor(x => x.VolumeUnit)
                    .NotNull()
                    .WithMessage(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Messages.Required,
                            nameof(CargoSpecifications.VolumeUnit)))
                    .IsInEnum();
            });

        RuleFor(x => x.Dimensions)
            .SetNonNullableValidator(new DimensionsValidator())
            .When(x => x.Dimensions is not null);

        RuleFor(x => x.WeightUnit)
            .NotNull()
            .WithMessage(
                string.Format(
                    CultureInfo.CurrentCulture,
                    Messages.Required,
                    nameof(CargoSpecifications.WeightUnit)))
            .IsInEnum();

        RuleFor(x => x.Weight)
            .NotEmpty()
            .WithMessage(
                string.Format(CultureInfo.CurrentCulture,
                    Messages.Required,
                    nameof(CargoSpecifications.Weight)))
            .GreaterThan(0);
    }
}
