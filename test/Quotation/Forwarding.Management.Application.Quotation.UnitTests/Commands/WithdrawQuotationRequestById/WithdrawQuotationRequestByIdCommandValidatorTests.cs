using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;
using Moq;

namespace Forwarding.Management.Application.Quotation.UnitTests.Commands.WithdrawQuotationRequestById;

public class WithdrawQuotationRequestByIdCommandValidatorTests
{
    private readonly Mock<IWithdrawQuotationRequestByIdStorageService> storageServiceMock;

    public WithdrawQuotationRequestByIdCommandValidatorTests()
    {
        storageServiceMock = new Mock<IWithdrawQuotationRequestByIdStorageService>();
    }

    [Fact]
    public async Task ValidateAsync_NonExistingItem_IsInvalid()
    {
        var command = new WithdrawQuotationRequestByIdCommand { Id = "Does-Not-Exist-Item-Id" };

        storageServiceMock
            .Setup(x => x.IsItemWithIdExistingAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(false);

        var validator = new WithdrawQuotationRequestByIdCommandValidator(storageServiceMock.Object);

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_ItemIsNotInPendingStatus_IsInvalid()
    {
        var command = new WithdrawQuotationRequestByIdCommand { Id = "Not-Pending-Status-Item-Id" };

        storageServiceMock
            .Setup(x => x.IsItemWithIdExistingAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(true);

        storageServiceMock
            .Setup(x => x.IsInPendingStatusAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(false);

        var validator = new WithdrawQuotationRequestByIdCommandValidator(storageServiceMock.Object);

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_ItemIsValid_ValidationIsPassed()
    {
        var command = new WithdrawQuotationRequestByIdCommand { Id = "Valid-Item-Id" };

        storageServiceMock
            .Setup(x => x.IsItemWithIdExistingAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(true);

        storageServiceMock
            .Setup(x => x.IsInPendingStatusAsync(command.Id, CancellationToken.None))
            .ReturnsAsync(true);

        var validator = new WithdrawQuotationRequestByIdCommandValidator(storageServiceMock.Object);

        var result = await validator.ValidateAsync(command, CancellationToken.None);

        Assert.True(result.IsValid);
    }
}
