using System.Globalization;
using System.Net;
using Forwarding.Management.Application.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Quotation;
public class DeleteQuotationRequestByIdTests : IClassFixture<WebApplicationFactory<Program>>
{

    private readonly WebApplicationFactory<Program> factory;
    private const string url = "api/quotations/requests/";

    public DeleteQuotationRequestByIdTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory
            .WithWebHostBuilder(builder => builder.UseEnvironment("Local"));
    }

    [Fact]
    public async Task DeleteQuotationRequest_ValidGuid_ShouldBe400()
    {
        //Arrange
        var client = factory.CreateClient();
        var quotationRequestId = Guid.NewGuid();

        var firstError = string.Format(
            CultureInfo.CurrentCulture,
            Messages.QuotationRequestShouldExists,
            quotationRequestId);

        //Act
        var response = await client.DeleteAsync(url + quotationRequestId, CancellationToken.None);
        var deserializedResponse =
            JsonConvert.DeserializeObject<ProblemDetails>(
                await response.Content.ReadAsStringAsync());

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(deserializedResponse?.Detail, $"{firstError}");
    }
}