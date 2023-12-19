using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Responses;
using Forwarding.Management.Domain.Enums;
using MediatR;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;

public record ListQuotationRequestsQuery : IRequest<ListQuotationRequestsQueryResponse>
{
    public string? FilterString { get; init; }
    public CargoType[]? FilterTypesOfCargo { get; init; }
    public TransportationMode[]? TypesOfTransportationMode { get; init; }
    public DateTimeOffset? FilterCreationDateFrom { get; init; }
    public DateTimeOffset? FilterCreationDateTo { get; init; }
    public double? FilterTotalWeightMin { get; init; }
    public double? FilterTotalWeightMax { get; init; }
    public double? FilterTotalDimensionsMin { get; init; }
    public double? FilterTotalDimensionsMax { get; init; }
    public QuotationRequestStatus[]? FilterStatuses { get; init; }
    public bool? FilterPriorityShipment { get; init; }
    public string? PageContinuationToken { get; init; }
    public int? PageMaxItems { get; init; }
    public QuotationRequestSortProperty? SortByProperty { get; set; }
    public SortDirection? SortDirection { get; set; }
}
