using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;

public class WithdrawQuotationRequestByIdCommandHandler : IRequestHandler<WithdrawQuotationRequestByIdCommand>
{
    private readonly IWithdrawQuotationRequestByIdStorageService storageService;
    private readonly ILogger<WithdrawQuotationRequestByIdCommandHandler> logger;

    public WithdrawQuotationRequestByIdCommandHandler(
        IWithdrawQuotationRequestByIdStorageService storageService,
        ILogger<WithdrawQuotationRequestByIdCommandHandler> logger)
    {
        this.storageService = storageService;
        this.logger = logger;
    }

    public async Task Handle(WithdrawQuotationRequestByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to withdraw quotation request with Id '{Id}'.", request.Id);

        await storageService.UpdateAsync(request.Id, cancellationToken);

        logger.LogInformation("Quotation request with Id '{Id}' has been successfully withdrawn.", request.Id);
    }
}
