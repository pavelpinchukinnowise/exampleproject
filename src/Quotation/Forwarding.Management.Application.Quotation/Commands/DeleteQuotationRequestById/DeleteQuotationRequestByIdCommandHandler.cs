using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;

public class DeleteQuotationRequestByIdCommandHandler
    : IRequestHandler<DeleteQuotationRequestByIdCommand, DeleteQuotationRequestByIdCommandResponse>
{
    private readonly ILogger<DeleteQuotationRequestByIdCommandHandler> logger;
    private readonly IDeleteQuotationRequestByIdService deleteQuotationRequestService;

    public DeleteQuotationRequestByIdCommandHandler(
        IDeleteQuotationRequestByIdService deleteQuotationRequestService,
        ILogger<DeleteQuotationRequestByIdCommandHandler> logger)
    {
        this.logger = logger;
        this.deleteQuotationRequestService = deleteQuotationRequestService;
    }
    public async Task<DeleteQuotationRequestByIdCommandResponse> Handle(
        DeleteQuotationRequestByIdCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Attempting to delete quotation request with Id '{Id}'.", request.Id);

        var quotationRequest = await deleteQuotationRequestService.DeleteByIdAsync(request.Id, cancellationToken);

        logger.LogInformation(
            "Quotation request with Id '{Id}' has been successfully deleted.", request.Id);

        return new DeleteQuotationRequestByIdCommandResponse()
        {
            Id = quotationRequest.Id,
            Cargo = quotationRequest.Cargo,
            StartingLocation = quotationRequest.StartingLocation,
            FinalLocation = quotationRequest.FinalLocation,
            IsPriorityShipment = quotationRequest.IsPriorityShipment,
            TransportationMode = quotationRequest.TransportationMode,
            Status = quotationRequest.Status,
            CreatedAtTimestamp = quotationRequest.CreatedAtTimestamp,
        };
    }
}
