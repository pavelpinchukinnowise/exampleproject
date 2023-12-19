using AutoFixture;
using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
using Forwarding.Management.Domain.Quotation.Model;
using Moq;

namespace Forwarding.Management.Application.Quotation.UnitTests.Queries.ListQuotationRequests;

public class ListQuotationRequestsHandlerTests
{
    private readonly Fixture fixture;
    private readonly Mock<IListQuotationRequestsStorageService> serviceMock;

    public ListQuotationRequestsHandlerTests()
    {
        fixture = new Fixture();
        serviceMock = new Mock<IListQuotationRequestsStorageService>();
    }

    [Fact]
    public async void InvokingHandler_ValidQuery_QuotationStorageReaderServiceInvokedOnce()
    {
        serviceMock
            .Setup(m => m.GetQuotationRequestsAsync(
                It.IsAny<QuotationRequestFilterOptions?>(),
                It.IsAny<PageOptions?>(),
                It.IsAny<QuotationRequestsSortingOptions?>(),
                CancellationToken.None))
            .ReturnsAsync(new Page<QuotationRequest>
            {
                Items = fixture.Create<IReadOnlyCollection<QuotationRequest>>(),
                ContinuationToken = null
            });

        var handler = new ListQuotationRequestsQueryHandler(serviceMock.Object);

        var query = new ListQuotationRequestsQuery();

        await handler.Handle(query, CancellationToken.None);

        serviceMock.Verify(
            s => s.GetQuotationRequestsAsync(
                It.IsAny<QuotationRequestFilterOptions?>(),
                It.IsAny<PageOptions?>(),
                It.IsAny<QuotationRequestsSortingOptions?>(), CancellationToken.None),
                Times.Once);
    }

    [Fact]
    public async Task Handle_ValidQuery_ItemsAndContinuationTokenAreReturned()
    {
        var pageResponse = new Page<QuotationRequest>
        {
            Items = fixture.Create<IReadOnlyCollection<QuotationRequest>>(),
            ContinuationToken = "Continuation-Token",
        };

        serviceMock
            .Setup(m => m.GetQuotationRequestsAsync(
                It.IsAny<QuotationRequestFilterOptions?>(),
                It.IsAny<PageOptions?>(),
                It.IsAny<QuotationRequestsSortingOptions?>(),
                CancellationToken.None))
            .ReturnsAsync(pageResponse);

        var handler = new ListQuotationRequestsQueryHandler(serviceMock.Object);

        var query = new ListQuotationRequestsQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(pageResponse.Items.Count, result.Items.Count);
        Assert.Equal(pageResponse.ContinuationToken, result.ContinuationToken);
    }
}
