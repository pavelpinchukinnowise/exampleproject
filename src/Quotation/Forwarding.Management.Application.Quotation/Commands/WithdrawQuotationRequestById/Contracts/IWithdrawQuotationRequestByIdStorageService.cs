namespace Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;

public interface IWithdrawQuotationRequestByIdStorageService
{
    Task UpdateAsync(string id, CancellationToken cancellationToken);

    Task<bool> IsItemWithIdExistingAsync(string id, CancellationToken cancellationToken);

    Task<bool> IsInPendingStatusAsync(string id, CancellationToken cancellationToken);
}
