using FastEndpoints;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationByTerm;
using Forwarding.Management.Domain.Location.Model;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Locations;

public class GetLocationsByTermEndpoint : Endpoint<GetLocationsByTermQuery>
{
    private readonly IMediator mediator;

    public GetLocationsByTermEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("locations");
        AllowAnonymous();
        Description(b => b
            .Produces<GeoLocation[]>(200, "application/json")
            .ProducesProblemFE()
            .ProducesProblemFE<InternalErrorResponse>(500)
        );
    }

    public override async Task HandleAsync(GetLocationsByTermQuery req, CancellationToken ct)
    {
        var response = await mediator.Send(req, ct);

        await SendAsync(response, cancellation: ct);
    }
}