using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Ports;

public class GetPortsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public GetPortsTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("1")]
    public async Task GetPorts_ValidRequest_ShouldBe200(string query,
        CancellationToken cancellationToken = default)
    {
        //Arrange
        var url = "api/ports";

        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync($"{url}?Query={query}", cancellationToken);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData(null)]
    public async Task GetPorts_InValidRequest_ShouldBe400(string query,
        CancellationToken cancellationToken = default)
    {
        //Arrange
        var url = "api/ports";

        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync($"{url}?Query={query}", cancellationToken);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetPorts_ToLongSearchString_ShouldBe400()
    {
        //Arrange
        var url = "api/ports";

        var longString = new string(Enumerable.Repeat('1', 513).Select(x => x).ToArray());
        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync($"{url}?Query={longString}", CancellationToken.None);

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}