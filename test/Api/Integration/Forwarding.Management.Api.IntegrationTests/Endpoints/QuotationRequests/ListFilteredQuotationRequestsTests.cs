using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Quotation;
public class ListFilteredQuotationRequestsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private const string url = "api/quotations/requests";

    public ListFilteredQuotationRequestsTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory
            .WithWebHostBuilder(builder => builder.UseEnvironment("Local"));
    }

    [Fact]
    public async Task ListFilteredQuotationRequests_ValidRequest_ShouldHasValidResponse()
    {
        //Arrange
        var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync(url, CancellationToken.None);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
