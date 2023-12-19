using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;
using Forwarding.Management.Domain.Location.Model;
using Moq;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsConnection;

public class GetLocationsConnectionFixture
{
    public virtual async Task<GetLocationsConnectionResponse> ExecuteRequestAsync(
        GetLocationsConnectionQuery query,
        CancellationToken cancellationToken = default)
    {
        var service = CreateGeoLocationService();

        var handler = new GetLocationsConnectionQueryHandler(service);

        return await handler.Handle(query, cancellationToken);
    }

    private static IGeoLocationService CreateGeoLocationService()
    {
        var moqGeolocationService = new Mock<IGeoLocationService>();

        moqGeolocationService
            .Setup(x =>
                x.IsPossibleLandConnectionAsync(It.IsAny<Coordinate>(), It.IsAny<Coordinate>(),CancellationToken.None))
            .ReturnsAsync(true);

        return moqGeolocationService.Object;
    }
}
