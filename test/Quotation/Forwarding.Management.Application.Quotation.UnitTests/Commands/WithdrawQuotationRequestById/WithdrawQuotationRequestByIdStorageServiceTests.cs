using AutoFixture;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Moq;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.WithdrawQuotationRequestById;

public class WithdrawQuotationRequestByIdStorageServiceTests
{
    private readonly Mock<CosmosClient> cosmosClientMock;
    private readonly Mock<Container> cosmosContainerMock;

    public WithdrawQuotationRequestByIdStorageServiceTests()
    {
        cosmosClientMock = new Mock<CosmosClient>();
        var cosmosDb = new Mock<Database>();
        cosmosContainerMock = new Mock<Container>();

        cosmosDb.Setup(x => x.GetContainer(It.IsAny<string>())).Returns(cosmosContainerMock.Object);
        cosmosClientMock.Setup(x => x.GetDatabase(It.IsAny<string>())).Returns(cosmosDb.Object);
    }

    [Fact]
    public async Task UpdateAsync_PassItem_SaveItemWithWithdrawnStatus()
    {
        const string id = "test-id";
        var response = new Mock<ItemResponse<QuotationRequest>>();
        var fixture = new Fixture();

        response.Setup(x => x.Resource).Returns(fixture.Create<QuotationRequest>());

        cosmosContainerMock
            .Setup(
                x => x.ReadItemAsync<QuotationRequest>(
                    id,
                    PartitionKey.Null,
                    null,
                    CancellationToken.None))
            .ReturnsAsync(response.Object);

        var storageService = new WithdrawQuotationRequestByIdStorageService(cosmosClientMock.Object);

        await storageService.UpdateAsync(id, CancellationToken.None);

        cosmosContainerMock.Verify(
            x => x.UpsertItemAsync(
                It.Is<QuotationRequest>(
                    r => r.Status == QuotationRequestStatus.Withdrawn
                         && r.StatusModifications.Any(m => m.Status == QuotationRequestStatus.Withdrawn)),
                null,
                null,
                CancellationToken.None));
    }
}
