using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class QuotationRequest : Entity
{
    public required Cargo Cargo { get; set; }
    public required TransportationMode TransportationMode { get; set; }
    public required Location StartingLocation { get; set; }
    public required Location FinalLocation { get; set; }
    public required DateTimeOffset CreatedAtTimestamp { get; set; }
    public required QuotationRequestStatus Status { get; set; }
    public required ICollection<StatusModification<QuotationRequestStatus>> StatusModifications { get; set; } = new List<StatusModification<QuotationRequestStatus>>();
    public required bool IsPriorityShipment { get; set; }
}
