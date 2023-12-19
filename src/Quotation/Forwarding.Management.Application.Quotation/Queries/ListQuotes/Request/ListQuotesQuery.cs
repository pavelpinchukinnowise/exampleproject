using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Response;
using MediatR;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes.Request;
public class ListQuotesQuery : IRequest<ListQuotesQueryResponse>
{
    public string? PageContinuationToken { get; init; }
    public int? PageMaxItems { get; init; }
}
