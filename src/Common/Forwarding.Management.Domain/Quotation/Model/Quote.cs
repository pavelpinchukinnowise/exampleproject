using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;
public class Quote : Entity
{
    public required Cargo Cargo { get; set; }
    public required TransportationMode TransportationMode { get; set; }
    public required Location StartingLocation { get; set; }
    public required Location FinalLocation { get; set; }
    public required DateTimeOffset CreatedAtTimestamp { get; set; }
    public required QuoteStatus Status { get; set; }
    public required ICollection<StatusModification<QuoteStatus>> StatusModifications { get; set; } = new List<StatusModification<QuoteStatus>>();
    public required bool IsPriorityShipment { get; set; }
    public required string Number { get; set; }
    public decimal ExchangeRate { get; set; }
    public required string Currency { get; set; }
    public DateTimeOffset ValidUntil { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalCostExcludingVAT { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalPriceExcludingVAT { get; set; }
}
