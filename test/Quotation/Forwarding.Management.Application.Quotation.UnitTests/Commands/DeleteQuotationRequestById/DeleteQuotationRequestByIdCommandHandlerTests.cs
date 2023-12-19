using AutoFixture;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Extensions.Logging;
using Moq;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.DeleteQuotationRequestById;
public class DeleteQuotationRequestByIdCommandHandlerTests
{
    private readonly Fixture fixture;

    public DeleteQuotationRequestByIdCommandHandlerTests()
    {
        fixture = new Fixture();
    }

    [Fact]
    public async void InvokingHandler_ValidCommand_StorageServiceDeleteByIdAsyncOnce()
    {
        var deleteStorageServiceMoq = new Mock<IDeleteQuotationRequestByIdService>();
        var loggerMoq = new Mock<ILogger<DeleteQuotationRequestByIdCommandHandler>>();

        deleteStorageServiceMoq
            .Setup(m => m.DeleteByIdAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(fixture.Create<QuotationRequest>);

        var handler = new DeleteQuotationRequestByIdCommandHandler(deleteStorageServiceMoq.Object, loggerMoq.Object);

        var command = fixture.Create<DeleteQuotationRequestByIdCommand>();

        await handler.Handle(command, CancellationToken.None);

        deleteStorageServiceMoq.Verify(s => s.DeleteByIdAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
    }
}
