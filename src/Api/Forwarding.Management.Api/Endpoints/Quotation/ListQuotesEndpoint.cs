using FastEndpoints;
using Forwarding.Management.Api.Constants;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Request;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Response;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Quotation;
public class ListQuotesEndpoint : Endpoint<ListQuotesQuery>
{
    private readonly IMediator mediator;

    public ListQuotesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("quotes/");
        AllowAnonymous();
        Description(b => b
            .Produces<IReadOnlyCollection<QuotesItem>>(StatusCodes.Status200OK, "application/json")
            .ProducesProblemFE());
    }

    public override async Task HandleAsync(ListQuotesQuery req, CancellationToken ct)
    {
        var result = await mediator.Send(req, ct);

        HttpContext.Response.Headers.Add(HttpConstants.ContinuationTokenHeader, result.ContinuationToken);

        await SendOkAsync(result.Items, ct);
    }
}
