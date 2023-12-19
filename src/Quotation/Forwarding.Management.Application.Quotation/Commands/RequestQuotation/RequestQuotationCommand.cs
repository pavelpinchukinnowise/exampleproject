using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using MediatR;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation;

public record RequestQuotationCommand : IRequest<RequestQuotationCommandResponse>
{
    public required Cargo Cargo { get; init; }

    public required TransportationMode TransportationMode { get; init; }

    public required Location StartingLocation { get; init; }

    public required Location FinalLocation { get; init; }

    public bool IsPriorityShipment { get; set; }
}
