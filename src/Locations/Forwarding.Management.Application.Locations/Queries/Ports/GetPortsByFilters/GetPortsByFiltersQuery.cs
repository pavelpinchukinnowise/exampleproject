using Forwarding.Management.Application.Locations.Queries.Ports.DTOs;
using Forwarding.Management.Domain.Enums;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;

public record GetPortsByFiltersQuery : IRequest<IReadOnlyCollection<PortDto>>
{
    public required string Query { get; init; }
    public TransportationMode? TransportationMode { get; set; }
}