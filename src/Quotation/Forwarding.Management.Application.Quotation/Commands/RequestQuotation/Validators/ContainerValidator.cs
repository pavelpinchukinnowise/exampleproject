using FluentValidation;
using Forwarding.Management.Application.Resources;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;

public class ContainerValidator : AbstractValidator<Container>
{
    public ContainerValidator()
    {
        RuleFor(x => x.Specifications).SetValidator(new CargoSpecificationsValidator());

        RuleFor(x => x.Specifications)
            .Must(x => x is { Dimensions: null })
            .When(x => x.LoadingMode == ContainerLoadingMode.Fcl)
            .WithMessage(Messages.DimensionsShouldBeNullIfFcl);
    }
}
