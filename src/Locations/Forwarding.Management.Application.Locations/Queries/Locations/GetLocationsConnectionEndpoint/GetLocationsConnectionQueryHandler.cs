using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;

public class GetLocationsConnectionQueryHandler
    : IRequestHandler<GetLocationsConnectionQuery, GetLocationsConnectionResponse>
{
    private readonly IGeoLocationService geoLocationService;

    public GetLocationsConnectionQueryHandler(IGeoLocationService geoLocationService)
    {
        this.geoLocationService = geoLocationService;
    }

    public async Task<GetLocationsConnectionResponse> Handle(
        GetLocationsConnectionQuery request,
        CancellationToken cancellationToken)
    {
        var startLocation = new Coordinate
        {
            Latitude = request.StartPointLatitude,
            Longitude = request.StartPointLongitude
        };

        var endLocation = new Coordinate
        {
            Latitude = request.EndPointLatitude,
            Longitude = request.EndPointLongitude
        };

        var land = await geoLocationService.IsPossibleLandConnectionAsync(
            startLocation,
            endLocation,
            cancellationToken);

        return new GetLocationsConnectionResponse { Land = land };
    }
}
