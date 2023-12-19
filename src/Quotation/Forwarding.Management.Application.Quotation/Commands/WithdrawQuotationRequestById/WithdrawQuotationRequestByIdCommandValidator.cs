using System.Globalization;
using System.Net;
using FluentValidation;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;
using Forwarding.Management.Application.Resources;

namespace Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;

public class WithdrawQuotationRequestByIdCommandValidator: AbstractValidator<WithdrawQuotationRequestByIdCommand>
{
    public WithdrawQuotationRequestByIdCommandValidator(IWithdrawQuotationRequestByIdStorageService storageService)
    {
        RuleFor(x => x.Id)
            .MustAsync(storageService.IsItemWithIdExistingAsync)
            .WithErrorCode(HttpStatusCode.NotFound.ToString())
            .WithMessage(cmd => string.Format(
                CultureInfo.CurrentCulture,
                Messages.QuotationRequestShouldExists,
                cmd.Id))
            .DependentRules(() => RuleFor(x => x.Id)
                .MustAsync(storageService.IsInPendingStatusAsync)
                .WithMessage(cmd => string.Format(
                    CultureInfo.CurrentCulture,
                    Messages.QuotationRequestInPendingStatus,
                    cmd.Id)));
    }
}
