using MediatR;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
public class DeleteQuotationRequestByIdCommand : IRequest<DeleteQuotationRequestByIdCommandResponse>
{
    public required string Id { get; set; }
}