using MediatR;

namespace Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;

public record WithdrawQuotationRequestByIdCommand : IRequest
{
    public required string Id { get; init; }
}
