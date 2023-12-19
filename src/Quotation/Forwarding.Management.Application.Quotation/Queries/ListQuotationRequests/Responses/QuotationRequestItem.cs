using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Responses;

public record QuotationRequestItem
{
    public required string Id { get; init; }

    public required Cargo Cargo { get; set; }

    public required TransportationMode TransportationMode { get; init; }

    public required Location StartingLocation { get; init; }

    public required Location FinalLocation { get; init; }

    public required DateTimeOffset CreatedAtTimestamp { get; init; }

    public required QuotationRequestStatus Status { get; init; }

    public required IReadOnlyCollection<StatusModification<QuotationRequestStatus>> StatusModifications { get; init; }

    public required bool IsPriorityShipment { get; init; }
}
