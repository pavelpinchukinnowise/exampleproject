using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsByCoordinates;

public class GetLocationsByCoordinatesTests : IClassFixture<GetLocationsByCoordinatesFixture>
{
    private readonly GetLocationsByCoordinatesFixture fixture;

    public GetLocationsByCoordinatesTests(GetLocationsByCoordinatesFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetLocationsCommandHandler_ValidRequest_ShouldReturnList()
    {
        //Arrange
        var query = new GetLocationsByCoordinatesQuery
        {
            Latitude = 0,
            Longitude = 0
        };

        //Act
        var res = await fixture.ExecuteRequestAsync(query, CancellationToken.None);

        //Assert
        Assert.NotNull(res);
        Assert.Equal(2, res.Count);
    }
}