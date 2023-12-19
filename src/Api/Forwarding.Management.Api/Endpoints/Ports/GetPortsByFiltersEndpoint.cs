using FastEndpoints;
using Forwarding.Management.Application.Locations.Queries.Ports.DTOs;
using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Ports;

public class GetPortsByFiltersEndpoint : Endpoint<GetPortsByFiltersQuery>
{
    private readonly IMediator mediator;

    public GetPortsByFiltersEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("ports");
        AllowAnonymous();
        Description(b => b
            .Produces<PortDto[]>(200, "application/json")
            .ProducesProblemFE()
            .ProducesProblemFE<InternalErrorResponse>(500));
    }

    public override async Task HandleAsync(GetPortsByFiltersQuery req, CancellationToken ct)
    {
        var response = await mediator.Send(req, ct);

        await SendAsync(response, cancellation: ct);
    }
}