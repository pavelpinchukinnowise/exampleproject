using Forwarding.Management.Application.Quotation.Contracts;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
public interface IDeleteQuotationRequestByIdService : IDeleteStorageService
{
    public Task<bool> IsInWithdrawnStatusAsync(
        string id,
        CancellationToken ct);

    public Task<bool> ItemExistsWithIdAsync(
        string id,
        CancellationToken ct);
}
