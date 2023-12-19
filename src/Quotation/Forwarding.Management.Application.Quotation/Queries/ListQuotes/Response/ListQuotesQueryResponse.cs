namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes.Response;
public class ListQuotesQueryResponse
{
    public required IReadOnlyCollection<QuotesItem> Items { get; set; }
    public string? ContinuationToken { get; init; }
}
