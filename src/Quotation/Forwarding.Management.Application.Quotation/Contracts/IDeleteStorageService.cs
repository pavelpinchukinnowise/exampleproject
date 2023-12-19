using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Contracts;
public interface IDeleteStorageService
{
    Task<QuotationRequest> DeleteByIdAsync(string id, CancellationToken ct);
}