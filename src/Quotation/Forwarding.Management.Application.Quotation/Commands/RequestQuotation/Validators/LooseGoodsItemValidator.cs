using FluentValidation;
using Forwarding.Management.Application.Common.FluentValidation;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation.Validators;

public class LooseGoodsItemValidator : AbstractValidator<LooseGoodsItem>
{
    public LooseGoodsItemValidator()
    {
        RuleFor(x => x.Specifications)
            .SetNonNullableValidator(new CargoSpecificationsValidator())
            .When(x => x.Specifications is not null);
    }
}
