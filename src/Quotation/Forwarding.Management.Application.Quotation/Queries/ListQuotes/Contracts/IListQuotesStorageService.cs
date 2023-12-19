using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes.Contracts;
public interface IListQuotesStorageService
{
    Task<Page<Quote>> GetQuotationsAsync(PageOptions pageOptions, CancellationToken cancellationToken);
}
