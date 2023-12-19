using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Ports.GetPortsByTerm;

public class GetPortsTests : IClassFixture<GetPortsFixture>
{
    private readonly GetPortsFixture fixture;

    public GetPortsTests(GetPortsFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetLocationsCommandHandler_ValidRequest_ShouldReturnList()
    {
        //Arrange
        var query = new GetPortsByFiltersQuery
        {
            Query = "123",
        };

        //Act
        var res = await fixture.ExecuteRequestAsync(query, CancellationToken.None);

        //Assert
        Assert.NotNull(res);
        Assert.Equal(2, res.Count);
    }
}