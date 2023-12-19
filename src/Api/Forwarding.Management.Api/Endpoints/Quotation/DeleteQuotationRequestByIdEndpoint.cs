using FastEndpoints;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Quotation;

public class DeleteQuotationRequestByIdEndpoint : Endpoint<DeleteQuotationRequestByIdCommand>
{
    private readonly IMediator mediator;

    public DeleteQuotationRequestByIdEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Delete("quotations/requests/{Id}");
        AllowAnonymous();
        Description(b => b
            .Produces(200)
            .Produces(404)
            .ProducesProblemFE()
            .ProducesProblemFE<InternalErrorResponse>(500));
    }

    public override async Task HandleAsync(DeleteQuotationRequestByIdCommand req, CancellationToken ct)
    {
        var quotationRequest = await mediator.Send(req, ct);

        await SendOkAsync(quotationRequest, ct);
    }
}