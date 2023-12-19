using Azure.Maps.Routing;
using Azure.Maps.Search;
using Forwarding.Management.Infrastructure.Locations.Services;
using Moq;

namespace Forwarding.Management.Application.Locations.UnitTests.Services;

public class GeoLocationServiceTests
{
    [Fact]
    public async Task GetLocationsByTermAsync_CorrectQuery_NotNull()
    {
        // Arrange
        var query = "pol";
        var mapsSearchClient = new Mock<MapsSearchClient>();
        var mapsRoutingClient = new Mock<MapsRoutingClient>();

        mapsSearchClient.Setup(api => api.SearchAddressAsync(query, default, default));

        var service = new GeoLocationService(mapsSearchClient.Object, mapsRoutingClient.Object);

        // Act
        var result = await service.GetLocationsByTermAsync(query);

        // Assert
        mapsSearchClient.Verify(m => m.SearchAddressAsync(query, default, default), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetLocationsByCoordinateAsync_CorrectQuery_NotNull()
    {
        // Arrange
        var mapsSearchClient = new Mock<MapsSearchClient>();
        var mapsRoutingClient = new Mock<MapsRoutingClient>();
        mapsSearchClient.Setup(m => m.ReverseSearchAddressAsync(It.IsAny<ReverseSearchOptions>(), default));

        var service = new GeoLocationService(mapsSearchClient.Object, mapsRoutingClient.Object);

        // Act
        var result = await service.GetLocationsByCoordinateAsync(23, 82);

        // Assert
        mapsSearchClient.Verify(m => m.ReverseSearchAddressAsync(It.Is<ReverseSearchOptions>(x =>
            x!.Coordinates!.Value!.Latitude == 23f &&
            x!.Coordinates!.Value.Longitude == 82f), default), Times.Once);
        Assert.NotNull(result);
    }
}
