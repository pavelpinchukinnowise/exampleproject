using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoFixture;
using FluentAssertions;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using QuotationRequestLocation = Forwarding.Management.Domain.Quotation.Model.Location;

namespace Forwarding.Management.Api.IntegrationTests.Endpoints.Quotation;

public class NewQuotationRequestTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const string Url = "api/quotations/requests";

    private readonly WebApplicationFactory<Program> factory;

    private readonly JsonSerializerOptions options;

    public NewQuotationRequestTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Local"));

        options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };
    }

    [Fact]
    public async Task NewQuotationRequest_ValidContainerizedRequest_ShouldHasValidResponse()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.Containerized,
                Containers = new List<Container>
                {
                    new()
                    {
                        Specifications = new CargoSpecifications
                        {
                            Weight = 10,
                            WeightUnit = WeightUnit.Kg
                        }
                    }
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        var deserializedResponse = JsonSerializer.Deserialize<RequestQuotationCommandResponse>(
            await response.Content.ReadAsStringAsync(),
            options);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(Guid.TryParse(deserializedResponse?.Id, out _));
        deserializedResponse.Cargo.Should().BeEquivalentTo(request.Cargo);
    }

    [Fact]
    public async Task NewQuotationRequest_ValidInBulkRequest_ShouldHasValidResponse()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.InBulk,
                BulkItems = new List<BulkItem> { new() { Name = fixture.Create<string>() } }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        var deserializedResponse = JsonSerializer.Deserialize<RequestQuotationCommandResponse>(
            await response.Content.ReadAsStringAsync(),
            options);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(Guid.TryParse(deserializedResponse?.Id, out _));
        deserializedResponse.Cargo.Should().BeEquivalentTo(request.Cargo);
    }

    [Fact]
    public async Task NewQuotationRequest_ValidLooseGoodsRequest_ShouldHasValidResponse()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.AsLooseGoods,
                LooseGoodsItems = new List<LooseGoodsItem>
                {
                    new()
                    {
                        Name = fixture.Create<string>(),
                        Specifications = new CargoSpecifications { Weight = 10, WeightUnit = WeightUnit.Kg }
                    }
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        var deserializedResponse = JsonSerializer.Deserialize<RequestQuotationCommandResponse>(
            await response.Content.ReadAsStringAsync(),
            options);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(Guid.TryParse(deserializedResponse?.Id, out _));
        deserializedResponse.Cargo.Should().BeEquivalentTo(request.Cargo);
    }

    [Fact]
    public async Task NewQuotationRequest_InvalidCargoType_ShouldBe400()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo { Type = CargoType.AsLooseGoods },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        request.Cargo.Type = CargoType.AsLooseGoods;
        request.Cargo.LooseGoodsItems = null;

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);
        var deserializedResponse = JsonSerializer.Deserialize<ProblemDetails>(
                await response.Content.ReadAsStringAsync());

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(!string.IsNullOrWhiteSpace(deserializedResponse?.Detail));
    }

    [Fact]
    public async Task NewQuotationRequest_ValidLooseGoodsCargo_ShouldBe200()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.AsLooseGoods,
                LooseGoodsItems = new List<LooseGoodsItem>
                {
                    new()
                    {
                        Name = fixture.Create<string>(),
                        Specifications = new CargoSpecifications { Weight = 10, WeightUnit = WeightUnit.Kg }
                    }
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task NewQuotationRequest_ValidInBulkCargo_ShouldBe200()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();

        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.InBulk,
                BulkItems = new List<BulkItem> { new() { Name = fixture.Create<string>() } }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task NewQuotationRequest_ValidContainerCargo_ShouldBe200()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();
        var request = new RequestQuotationCommand
        {
            Cargo = new Cargo
            {
                Type = CargoType.Containerized,
                Containers = new List<Container>
                {
                    new()
                    {
                        Specifications = new CargoSpecifications
                        {
                            Weight = 10,
                            WeightUnit = WeightUnit.Kg
                        }
                    }
                }
            },
            TransportationMode = fixture.Create<TransportationMode>(),
            StartingLocation = fixture.Create<QuotationRequestLocation>(),
            FinalLocation = fixture.Create<QuotationRequestLocation>()
        };

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task NewQuotationRequest_ProvideLooseGoodsAndBulkOnContainerizedType_ShouldBe400()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();
        var request = fixture.Create<RequestQuotationCommand>();
        request.Cargo.Type = CargoType.Containerized;

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);
        var deserializedResponse = JsonSerializer.Deserialize<ProblemDetails>(
                await response.Content.ReadAsStringAsync());
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(!string.IsNullOrWhiteSpace(deserializedResponse?.Detail));
    }

    [Fact]
    public async Task NewQuotationRequest_ProvideContainersAndBulkOnLooseGoodsType_ShouldBe400()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();
        var request = fixture.Create<RequestQuotationCommand>();
        request.Cargo.Type = CargoType.AsLooseGoods;

        SetDefaultLocation(request);

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);
        var deserializedResponse = JsonSerializer.Deserialize<ProblemDetails>(
                await response.Content.ReadAsStringAsync());
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(!string.IsNullOrWhiteSpace(deserializedResponse?.Detail));
    }

    [Fact]
    public async Task NewQuotationRequest_ProvideContainersAndLooseGoodsOnBulkType_ShouldBe400()
    {
        //Arrange
        var client = factory.CreateClient();
        var fixture = new Fixture();
        var request = fixture.Create<RequestQuotationCommand>();
        request.Cargo.Type = CargoType.InBulk;

        var serializedModel = JsonSerializer.Serialize(request);
        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
        //Act
        var response = await client.PostAsync(Url, content, CancellationToken.None);
        var deserializedResponse = JsonSerializer.Deserialize<ProblemDetails>(
                await response.Content.ReadAsStringAsync());
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(!string.IsNullOrWhiteSpace(deserializedResponse?.Detail));
    }

    private void SetDefaultLocation(RequestQuotationCommand request)
    {
        request.StartingLocation.Type = LocationType.Port;
        request.StartingLocation.PortId = 1;
        request.FinalLocation.Type = LocationType.Port;
        request.FinalLocation.PortId = 2;
    }
}
