using Forwarding.Management.Application.Quotation.Contracts;
using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation;

public class RequestQuotationCommandHandler : IRequestHandler<RequestQuotationCommand, RequestQuotationCommandResponse>
{
    private readonly ILogger<RequestQuotationCommandHandler> logger;
    private readonly IWriteStorageService<QuotationRequest> storageService;

    public RequestQuotationCommandHandler(
        ILogger<RequestQuotationCommandHandler> logger,
        IWriteStorageService<QuotationRequest> storageService)
    {
        this.logger = logger;
        this.storageService = storageService;
    }

    public async Task<RequestQuotationCommandResponse> Handle(RequestQuotationCommand request, CancellationToken cancellationToken)
    {
        var fromLocationType = GetLocationTypeDisplayName(request.StartingLocation.Type);
        var toLocationType = GetLocationTypeDisplayName(request.FinalLocation.Type);

        logger.LogInformation(
            "Quotation Request for shipping cargo '{CargoType}' and '{Itinerary}' is being handled.",
            request.Cargo.Type,
            $"{fromLocationType}-{toLocationType}");

        var createdTimestamp = DateTimeOffset.UtcNow;

        var result = await storageService.UpsertAsync(
            new QuotationRequest
            {
                Id = Guid.NewGuid().ToString(),
                Cargo = request.Cargo,
                TransportationMode = request.TransportationMode,
                StartingLocation = request.StartingLocation,
                FinalLocation = request.FinalLocation,
                Status = QuotationRequestStatus.Pending,
                StatusModifications =
                    new List<StatusModification<QuotationRequestStatus>>
                    {
                        new(QuotationRequestStatus.Pending, createdTimestamp)
                    },
                IsPriorityShipment = request.IsPriorityShipment,
                CreatedAtTimestamp = createdTimestamp
            },
            cancellationToken);

        logger.LogInformation(
            "Quotation Request for shipping cargo '{CargoType}' and '{Itinerary}' has been stored with Id '{Id}'.",
            result.Cargo.Type,
            $"{fromLocationType}-{toLocationType}",
            result.Id);

        return new RequestQuotationCommandResponse()
        {
            Id = result.Id,
            Cargo = result.Cargo,
            TransportationMode = result.TransportationMode,
            StartingLocation = result.StartingLocation,
            FinalLocation = result.FinalLocation,
            CreatedAtTimestamp = result.CreatedAtTimestamp,
            IsPriorityShipment = request.IsPriorityShipment
        };
    }

    private static string GetLocationTypeDisplayName(LocationType locationType)
    {
        return locationType switch
        {
            LocationType.Port => "Port",
            LocationType.Address => "Door",
            _ => throw new ArgumentOutOfRangeException(nameof(locationType))
        };
    }
}
