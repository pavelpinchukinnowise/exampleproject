using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;

public record GetLocationsByCoordinatesQuery : IRequest<IReadOnlyCollection<GeoLocation>>
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}