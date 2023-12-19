using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes.Response;
public class QuotesItem
{
    public required string Id { get; init; }
    public required Cargo Cargo { get; init; }
    public required TransportationMode TransportationMode { get; init; }
    public required Location StartingLocation { get; init; }
    public required Location FinalLocation { get; init; }
    public required DateTimeOffset CreatedAtTimestamp { get; init; }
    public required QuoteStatus Status { get; init; }
    public required ICollection<StatusModification<QuoteStatus>> StatusModifications { get; init; } = new List<StatusModification<QuoteStatus>>();
    public required bool IsPriorityShipment { get; init; }
    public required string Number { get; init; }
    public decimal ExchangeRate { get; init; }
    public required string Currency { get; init; }
    public DateTimeOffset ValidUntil { get; init; }
    public decimal TotalCost { get; set; }
    public decimal TotalCostExcludingVAT { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceExcludingVAT { get; set; }
}
