using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Locations.GetLocationsByTerm;

public class GetLocationsTests : IClassFixture<GetLocationsFixture>
{
    private readonly GetLocationsFixture fixture;

    public GetLocationsTests(GetLocationsFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetLocationsCommandHandler_ValidRequest_ShouldReturnList()
    {
        //Arrange
        var query = new GetLocationsByTermQuery
        {
            Query = "123"
        };

        //Act
        var res = await fixture.ExecuteRequestAsync(query, CancellationToken.None);

        //Assert
        Assert.NotNull(res);
        Assert.Equal(2, res.Count);
    }
}