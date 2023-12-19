using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using static Xunit.Assert;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Location;

public class GetLocationsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public GetLocationsTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Theory]
    [InlineData(null)]
    public async Task GetLocations_InValidRequest_ShouldBe400(string query,
        CancellationToken cancellationToken = default)
    {
        //Arrange
        var url = "api/locations";

        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync($"{url}?Query={query}", cancellationToken);

        //Assert
        Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetLocations_ToLongSearchString_ShouldBe400()
    {
        //Arrange
        var url = "api/locations";

        var longString = new string(Enumerable.Repeat('1', 513).Select(x => x).ToArray());
        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync($"{url}?Query={longString}", CancellationToken.None);

        //Assert
        Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}