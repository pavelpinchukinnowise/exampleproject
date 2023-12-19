using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Location;

public class GetLocationsByCoordinatesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public GetLocationsByCoordinatesTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Theory]
    [InlineData(500, 500)]
    public async Task GetLocations_InValidRequest_ShouldBe500(double lat, double lon,
        CancellationToken cancellationToken = default)
    {
        //Arrange
        var url = "api/locations";

        var client = factory.CreateClient();
        //Act
        var response = await client.GetAsync($"{url}/{lat}/{lon}", cancellationToken);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}