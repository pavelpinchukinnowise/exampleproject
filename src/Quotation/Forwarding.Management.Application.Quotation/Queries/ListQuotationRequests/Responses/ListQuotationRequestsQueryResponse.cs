namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Responses;

public record ListQuotationRequestsQueryResponse
{
    public required IReadOnlyCollection<QuotationRequestItem> Items { get; init; }

    public string? ContinuationToken { get; init; }
}
