using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;

public record GetLocationsConnectionQuery : IRequest<GetLocationsConnectionResponse>
{
    public required double StartPointLatitude { get; init; }

    public required double StartPointLongitude { get; init; }

    public required double EndPointLatitude { get; init; }

    public required double EndPointLongitude { get; init; }
}
