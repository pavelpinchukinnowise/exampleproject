using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Responses;
using MediatR;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests;

public class ListQuotationRequestsQueryHandler
    : IRequestHandler<ListQuotationRequestsQuery, ListQuotationRequestsQueryResponse>
{
    private readonly IListQuotationRequestsStorageService storageService;

    public ListQuotationRequestsQueryHandler(IListQuotationRequestsStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<ListQuotationRequestsQueryResponse> Handle(
        ListQuotationRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var filters = new QuotationRequestFilterOptions
        {
            Statuses = request.FilterStatuses,
            CreationDateRangeStart = request.FilterCreationDateFrom,
            CreationDateRangeEnd = request.FilterCreationDateTo,
            TotalDimensionsRangeEnd = request.FilterTotalDimensionsMax,
            TotalDimensionsRangeStart = request.FilterTotalDimensionsMin,
            TotalWeightRangeEnd = request.FilterTotalWeightMax,
            TotalWeightRangeStart = request.FilterTotalWeightMin,
            SearchPriorityShipment = request.FilterPriorityShipment,
            TypesOfCargo = request.FilterTypesOfCargo,
            SearchString = request.FilterString,
            TypesOfTransportationMode = request.TypesOfTransportationMode,
        };

        var pageOptions = new PageOptions
        {
            ContinuationToken = request.PageContinuationToken,
            MaxPerPage = request.PageMaxItems
        };

        var sortingOptions = new QuotationRequestsSortingOptions
        {
            SortByProperty = request.SortByProperty,
            SortDirection = request.SortDirection,
        };

        var pageResponse = await storageService.GetQuotationRequestsAsync(
            filters,
            pageOptions,
            sortingOptions,
            cancellationToken);

        var quotationRequests = pageResponse.Items.Select(item => new QuotationRequestItem
        {
            Id = item.Id,
            FinalLocation = item.FinalLocation,
            StartingLocation = item.StartingLocation,
            TransportationMode = item.TransportationMode,
            Cargo = item.Cargo,
            CreatedAtTimestamp = item.CreatedAtTimestamp,
            Status = item.Status,
            StatusModifications = item.StatusModifications.ToList(),
            IsPriorityShipment = item.IsPriorityShipment
        });

        return new ListQuotationRequestsQueryResponse
        {
            Items = quotationRequests.ToList(),
            ContinuationToken = pageResponse.ContinuationToken
        };
    }
}
