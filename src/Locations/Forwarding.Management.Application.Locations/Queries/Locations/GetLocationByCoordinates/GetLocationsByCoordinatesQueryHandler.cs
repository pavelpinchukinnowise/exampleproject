using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;

public class GetLocationsByCoordinatesQueryHandler : IRequestHandler<GetLocationsByCoordinatesQuery,
    IReadOnlyCollection<GeoLocation>>
{
    private readonly IGeoLocationService geoLocationService;

    public GetLocationsByCoordinatesQueryHandler(IGeoLocationService geoLocationService)
    {
        this.geoLocationService = geoLocationService;
    }

    public async Task<IReadOnlyCollection<GeoLocation>> Handle(GetLocationsByCoordinatesQuery request,
        CancellationToken cancellationToken)
        => await geoLocationService.GetLocationsByCoordinateAsync(request.Latitude!,
            request.Longitude, cancellationToken);
}