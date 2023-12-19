using System.Globalization;
using FluentValidation;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
using Forwarding.Management.Application.Resources;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;

public class DeleteQuotationRequestByIdCommandValidator : AbstractValidator<DeleteQuotationRequestByIdCommand>
{

    public DeleteQuotationRequestByIdCommandValidator(IDeleteQuotationRequestByIdService deleteQuotationRequestService)
    {
        RuleFor(x => x.Id)
            .MustAsync(deleteQuotationRequestService.ItemExistsWithIdAsync)
            .WithErrorCode("NotFound")
            .WithMessage(cmd => string.Format(
                CultureInfo.CurrentCulture,
                Messages.QuotationRequestShouldExists,
                cmd.Id))
            .DependentRules(() =>
                RuleFor(x => x.Id)
                    .MustAsync(deleteQuotationRequestService.IsInWithdrawnStatusAsync)
                    .WithMessage(cmd => string.Format(
                        CultureInfo.CurrentCulture,
                        Messages.QuotationRequestInWithdrawnStatus,
                        cmd.Id)));
    }
}