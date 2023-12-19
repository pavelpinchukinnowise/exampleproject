using AutoFixture;
using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Request;
using Forwarding.Management.Domain.Quotation.Model;
using Moq;

namespace Forwarding.Management.Application.Quotation.UnitTests.Queries.ListQuotes;

public class ListQuotesQueryHandlerTests
{
    private readonly Fixture fixture;
    private readonly Mock<IListQuotesStorageService> serviceMock;

    public ListQuotesQueryHandlerTests()
    {
        fixture = new Fixture();
        serviceMock = new Mock<IListQuotesStorageService>();
    }

    [Fact]
    public async void InvokingHandler_ValidQuery_QuotationStorageReaderServiceInvokedOnce()
    {
        serviceMock
            .Setup(m => m.GetQuotationsAsync(It.IsAny<PageOptions>(),
                CancellationToken.None))
            .ReturnsAsync(new Page<Quote>
            {
                Items = fixture.Create<List<Quote>>(),
                ContinuationToken = null
            });

        var handler = new ListQuotesQueryHandler(serviceMock.Object);

        var query = new ListQuotesQuery();

        await handler.Handle(query, CancellationToken.None);

        serviceMock.Verify(
            s => s.GetQuotationsAsync(
                It.IsAny<PageOptions>(),
                CancellationToken.None),
                Times.Once);
    }

    [Fact]
    public async Task Handle_ValidQuery_ItemsAndContinuationTokenAreReturned()
    {
        var pageResponse = new Page<Quote>
        {
            Items = fixture.Create<IReadOnlyCollection<Quote>>(),
            ContinuationToken = "Continuation-Token",
        };

        serviceMock
            .Setup(m => m.GetQuotationsAsync(
                It.IsAny<PageOptions>(),
                CancellationToken.None))
            .ReturnsAsync(pageResponse);

        var handler = new ListQuotesQueryHandler(serviceMock.Object);

        var query = new ListQuotesQuery();

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(pageResponse.Items.Count, result.Items.Count);
        Assert.Equal(pageResponse.ContinuationToken, result.ContinuationToken);
    }
}
