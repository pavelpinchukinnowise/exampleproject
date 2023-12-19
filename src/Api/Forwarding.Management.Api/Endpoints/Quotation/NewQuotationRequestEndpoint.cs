using FastEndpoints;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation;
using MediatR;

namespace Forwarding.Management.Api.Endpoints.Quotation;

public class NewQuotationRequestEndpoint : Endpoint<RequestQuotationCommand>
{
    private readonly ILogger<NewQuotationRequestEndpoint> logger;
    private readonly IMediator mediator;

    public NewQuotationRequestEndpoint(IMediator mediator, ILogger<NewQuotationRequestEndpoint> logger)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    public override void Configure()
    {
        Post("quotations/requests");
        AllowAnonymous();
        Description(b => b
            .Produces(200)
            .ProducesProblemFE()
            .ProducesProblemFE<InternalErrorResponse>(500));
    }

    public override async Task HandleAsync(RequestQuotationCommand req, CancellationToken ct)
    {
        HttpContext.Request.Body.Position = 0;

        using (var stream = new StreamReader(HttpContext.Request.Body))
        {
            var request = await stream.ReadToEndAsync(ct);
            logger.LogInformation(
                "A new quotation request is going to be processed. Request body: {QuotationRequest}",
                request);
        }

        var result = await mediator.Send(req, ct);

        await SendOkAsync(result, ct);
    }
}
