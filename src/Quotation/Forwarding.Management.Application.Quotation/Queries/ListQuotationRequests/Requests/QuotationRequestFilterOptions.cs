
using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;

public record QuotationRequestFilterOptions
{
    public string? SearchString { get; init; }
    public CargoType[]? TypesOfCargo { get; init; }
    public TransportationMode[]? TypesOfTransportationMode { get; init; }
    public DateTimeOffset? CreationDateRangeStart { get; init; }
    public DateTimeOffset? CreationDateRangeEnd { get; init; }
    public double? TotalWeightRangeStart { get; init; }
    public double? TotalWeightRangeEnd { get; init; }
    public double? TotalDimensionsRangeStart { get; init; }
    public double? TotalDimensionsRangeEnd { get; init; }
    public QuotationRequestStatus[]? Statuses { get; init; }
    public bool? SearchPriorityShipment { get; init; }
}
