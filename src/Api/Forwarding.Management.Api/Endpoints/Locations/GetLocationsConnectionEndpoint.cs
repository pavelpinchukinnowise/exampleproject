using FastEndpoints;
using Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Locations;

public class GetLocationsConnectionEndpoint : Endpoint<GetLocationsConnectionQuery>
{
    private readonly IMediator mediator;

    public GetLocationsConnectionEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("locations/connections/{startPointLatitude},{startPointLongitude}/{endPointLatitude},{endPointLongitude}");
        AllowAnonymous();
        Description(b => b
            .Produces<GetLocationsConnectionResponse>()
            .ProducesProblemFE());
    }

    public override async Task HandleAsync(GetLocationsConnectionQuery req, CancellationToken ct)
    {
        var response = await mediator.Send(req, ct);

        await SendOkAsync(response, ct);
    }
}
