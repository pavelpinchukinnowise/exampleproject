using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;
using Forwarding.Management.Domain.Location.Model;
using Moq;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsByCoordinates;

public class GetLocationsByCoordinatesFixture
{
    protected static IGeoLocationService CreateGeoLocationService(GetLocationsByCoordinatesQuery query)
    {
        var moqGeolocationService =
            new Mock<IGeoLocationService>();
        moqGeolocationService
            .Setup(x =>
                x.GetLocationsByCoordinateAsync(query.Longitude, query.Latitude,
                    CancellationToken.None)).ReturnsAsync(GetLocationsDTO());
        return moqGeolocationService.Object;
    }

    public virtual async Task<IReadOnlyCollection<GeoLocation>> ExecuteRequestAsync(
        GetLocationsByCoordinatesQuery query, CancellationToken cancellationToken = default)
    {
        var geolocationService = CreateGeoLocationService(query);
        var handler = new GetLocationsByCoordinatesQueryHandler(geolocationService);

        return await handler.Handle(query, cancellationToken);
    }

    private static IReadOnlyCollection<GeoLocation> GetLocationsDTO() =>
        new GeoLocation[]
        {
            new()
            {
                Address = "frankw", Country = "Poland", Latitude = 123, Longitude = 57,
                PostalCode = "456", CountryCode = "PO"
            },
            new()
            {
                Address = "paris", Country = "France", Latitude = 34, Longitude = 78,
                PostalCode = "4567", CountryCode = "FR"
            }
        };
}