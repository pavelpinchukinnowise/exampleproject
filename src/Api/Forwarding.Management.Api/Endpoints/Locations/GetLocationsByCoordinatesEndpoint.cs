using FastEndpoints;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByCoordinates;
using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Locations;

public class GetLocationsByCoordinatesEndpoint : Endpoint<GetLocationsByCoordinatesQuery>
{
    private readonly IMediator mediator;

    public GetLocationsByCoordinatesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("locations/{Latitude}/{Longitude}");
        AllowAnonymous();
        Description(b => b
            .Produces<GeoLocation[]>(200, "application/json")
            .ProducesProblemFE()
            .ProducesProblemFE<GetLocationsByCoordinatesQuery>(500));
    }

    public override async Task HandleAsync(GetLocationsByCoordinatesQuery req, CancellationToken ct)
    {
        var response = await mediator.Send(req, ct);

        await SendAsync(response, cancellation: ct);
    }
}