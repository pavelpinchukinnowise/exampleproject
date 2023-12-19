using FastEndpoints;
using Forwarding.Management.Api.Constants;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Responses;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Quotation;

public class ListQuotationRequestsEndpoint : Endpoint<ListQuotationRequestsQuery>
{
    private readonly IMediator mediator;

    public ListQuotationRequestsEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("quotations/requests");
        AllowAnonymous();
        Description(b => b
            .Produces<IReadOnlyCollection<QuotationRequestItem>>(StatusCodes.Status200OK, "application/json")
            .ProducesProblemFE());
    }

    public override async Task HandleAsync(ListQuotationRequestsQuery req, CancellationToken ct)
    {
        var result = await mediator.Send(req, ct);

        HttpContext.Response.Headers.Add(HttpConstants.ContinuationTokenHeader, result.ContinuationToken);

        await SendOkAsync(result.Items, ct);
    }
}
