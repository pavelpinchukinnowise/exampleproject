using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Contracts;

public interface IListQuotationRequestsStorageService
{
    Task<Page<QuotationRequest>> GetQuotationRequestsAsync(
        QuotationRequestFilterOptions? filter,
        PageOptions? page,
        QuotationRequestsSortingOptions? sortingOptions,
        CancellationToken cancellationToken
    );
}
