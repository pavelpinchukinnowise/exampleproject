using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsConnection;

public class GetLocationsConnectionQueryHandlerTests : IClassFixture<GetLocationsConnectionFixture>
{
    private readonly GetLocationsConnectionFixture fixture;

    public GetLocationsConnectionQueryHandlerTests(GetLocationsConnectionFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetLocationsQueryHandler_ValidRequest_ShouldPossibleLandConnection()
    {
        //Arrange
        var query = new GetLocationsConnectionQuery
        {
            StartPointLatitude = 12,
            EndPointLatitude = 12,
            EndPointLongitude = 12,
            StartPointLongitude = 12
        };

        //Act
        var res = await fixture.ExecuteRequestAsync(query, CancellationToken.None);

        //Assert
        Assert.True(res.Land);
    }
}
