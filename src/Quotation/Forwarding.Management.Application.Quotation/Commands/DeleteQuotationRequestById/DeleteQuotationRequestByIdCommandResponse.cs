using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
public class DeleteQuotationRequestByIdCommandResponse
{
    public string? Id { get; set; }
    public required Cargo Cargo { get; set; }
    public required TransportationMode TransportationMode { get; set; }
    public required Location StartingLocation { get; set; }
    public required Location FinalLocation { get; set; }
    public DateTimeOffset CreatedAtTimestamp { get; set; }
    public QuotationRequestStatus Status { get; set; }
    public bool IsPriorityShipment { get; set; }
}
