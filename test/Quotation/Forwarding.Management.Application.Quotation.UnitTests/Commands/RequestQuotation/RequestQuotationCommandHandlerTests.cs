using AutoFixture;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation;
using Forwarding.Management.Application.Quotation.Contracts;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Extensions.Logging;
using Moq;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.RequestQuotation;

public class RequestQuotationCommandCommandHandlerTests
{
    private readonly Fixture fixture;

    public RequestQuotationCommandCommandHandlerTests()
    {
        fixture = new Fixture();
    }

    [Fact]
    public async void InvokingHandler_ValidCommand_StorageServiceUpsertInvokedOnce()
    {
        var storageServiceMock = new Mock<IWriteStorageService<QuotationRequest>>();

        storageServiceMock
            .Setup(m => m.UpsertAsync(It.IsAny<QuotationRequest>(), CancellationToken.None))
            .Returns(Task.FromResult(fixture.Create<QuotationRequest>()));

        var handler = new RequestQuotationCommandHandler(
            Mock.Of<ILogger<RequestQuotationCommandHandler>>(),
            storageServiceMock.Object);

        var command = fixture.Create<RequestQuotationCommand>();

        await handler.Handle(command, CancellationToken.None);

        storageServiceMock.Verify(s => s.UpsertAsync(It.IsAny<QuotationRequest>(), CancellationToken.None), Times.Once);
    }
}