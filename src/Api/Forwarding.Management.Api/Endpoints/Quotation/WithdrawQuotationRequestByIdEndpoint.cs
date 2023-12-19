using FastEndpoints;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Quotation;

public class WithdrawQuotationRequestByIdEndpoint: Endpoint<WithdrawQuotationRequestByIdCommand>
{
    private readonly IMediator mediator;

    public WithdrawQuotationRequestByIdEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Post("quotations/requests/{id}/withdraw");
        AllowAnonymous();

        Description(
            b => b
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblemFE()
                .ProducesProblemFE(StatusCodes.Status404NotFound),
            true);
    }

    public override async Task HandleAsync(WithdrawQuotationRequestByIdCommand req, CancellationToken ct)
    {
        await mediator.Send(req, ct);

        await SendNoContentAsync(ct);
    }
}
