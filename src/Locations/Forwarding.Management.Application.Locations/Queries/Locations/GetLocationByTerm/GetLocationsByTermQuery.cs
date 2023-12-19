using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;

public record GetLocationsByTermQuery : IRequest<IReadOnlyCollection<GeoLocation>>
{
    public required string Query { get; init; }
}