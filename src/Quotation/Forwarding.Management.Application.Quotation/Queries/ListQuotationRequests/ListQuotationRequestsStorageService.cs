using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Constants;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests;

public class ListQuotationRequestsStorageService : IListQuotationRequestsStorageService
{
    private readonly Container quotationRequestsContainer;

    public ListQuotationRequestsStorageService(CosmosClient cosmos)
    {
        quotationRequestsContainer = cosmos
            .GetDatabase(id: DatabaseConstants.DatabaseName)
            .GetContainer(id: DatabaseConstants.QuotationRequestsContainerName);
    }

    public async Task<Page<QuotationRequest>> GetQuotationRequestsAsync(
        QuotationRequestFilterOptions? filter,
        PageOptions? page,
        QuotationRequestsSortingOptions? sortingOptions,
        CancellationToken cancellationToken)
    {
        var requestOptions = new QueryRequestOptions { MaxItemCount = page?.MaxPerPage ?? -1 };

        var queryable = quotationRequestsContainer
            .GetItemLinqQueryable<QuotationRequest>(
                continuationToken: DecodeContinuationToken(page?.ContinuationToken),
                requestOptions: requestOptions)
            .AsQueryable();

        if (filter is not null)
        {
            ApplyFilters(filter, ref queryable);
        }

        if(sortingOptions is not null)
        {
            queryable = ApplySortProperty(queryable, sortingOptions.SortByProperty, sortingOptions.SortDirection);
        }

        var result = await queryable.ToFeedIterator().ReadNextAsync(cancellationToken);

        return new Page<QuotationRequest>
        {
            Items = result.Resource.ToList(),
            ContinuationToken = EncodeContinuationToken(result.ContinuationToken)
        };
    }

    private static void ApplyFilters(
        QuotationRequestFilterOptions filter,
        ref IQueryable<QuotationRequest> queryable)
    {
        if (filter.TypesOfCargo != null && filter.TypesOfCargo.Any())
        {
            queryable = queryable.Where(x => filter.TypesOfCargo.Contains(x.Cargo.Type));
        }

        if (filter.TypesOfTransportationMode != null && filter.TypesOfTransportationMode.Any())
        {
            queryable = queryable.Where(x => filter.TypesOfTransportationMode.Contains(x.TransportationMode));
        }

        if (filter.Statuses != null && filter.Statuses.Any())
        {
            queryable = queryable.Where(x => filter.Statuses.Contains(x.Status));
        }

        if (filter.SearchPriorityShipment.HasValue)
        {
            queryable = queryable.Where(x => x.IsPriorityShipment == filter.SearchPriorityShipment);
        }

        if (!string.IsNullOrWhiteSpace(filter.SearchString))
        {
            queryable = ApplySearchStringFilter(filter.SearchString, queryable);
        }

        if (filter.CreationDateRangeStart.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedAtTimestamp >= filter.CreationDateRangeStart);
        }

        if (filter.CreationDateRangeEnd.HasValue)
        {
            queryable = queryable.Where(x => x.CreatedAtTimestamp <= filter.CreationDateRangeEnd);
        }
    }

    private static IQueryable<QuotationRequest> ApplySearchStringFilter(
        string searchString,
        IQueryable<QuotationRequest> queryable)
    {
        queryable = queryable.Where(x =>
        //Starting location port
         (((x.StartingLocation.Type == LocationType.Port && x.StartingLocation.Port != null)
         && (x.StartingLocation.Port.Country
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.Port.CountryCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.Port.InternationalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.Port.Name
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || (x.StartingLocation.Port.PostalCode != null && x.StartingLocation.Port.PostalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
         || (x.StartingLocation.Port.Address != null && x.StartingLocation.Port.Address
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))))
         //Starting location addres
         || ((x.StartingLocation.Type == LocationType.Address && x.StartingLocation.GeoLocation != null)
         && (x.StartingLocation.GeoLocation.Country
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.GeoLocation.CountryCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.GeoLocation.Address
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.StartingLocation.GeoLocation.PostalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))))
         //Final location port
         || ((x.FinalLocation.Type == LocationType.Port && x.FinalLocation.Port != null)
         && (x.FinalLocation.Port.Country
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.Port.CountryCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.Port.InternationalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.Port.Name
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || (x.FinalLocation.Port.PostalCode != null && x.FinalLocation.Port.PostalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
         || (x.FinalLocation.Port.Address != null && x.FinalLocation.Port.Address
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))))
         //Final location addres
         || ((x.FinalLocation.Type == LocationType.Address && x.FinalLocation.GeoLocation != null)
         && (x.FinalLocation.GeoLocation.Country
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.GeoLocation.CountryCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.GeoLocation.Address
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
         || x.FinalLocation.GeoLocation.PostalCode
         .Contains(searchString, StringComparison.InvariantCultureIgnoreCase)))
         //Cargo
         || (x.Cargo.BulkItems != null && x.Cargo.BulkItems.Any()
         && (x.Cargo.BulkItems.Any(e => e.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))))

        || (x.Cargo.LooseGoodsItems != null && x.Cargo.LooseGoodsItems.Any()
         && (x.Cargo.LooseGoodsItems.Any(e => e.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)))));
        return queryable;
    }

    private static string? DecodeContinuationToken(string? continuationToken)
    {
        return continuationToken is not null
            ? Encoding.UTF8.GetString(Convert.FromBase64String(continuationToken))
            : null;
    }

    private static string? EncodeContinuationToken(string? continuationToken)
    {
        return continuationToken is not null
            ? Convert.ToBase64String(Encoding.UTF8.GetBytes(continuationToken))
            : null;
    }

    private static IQueryable<QuotationRequest> ApplySortProperty(
    IQueryable<QuotationRequest> queryable,
    QuotationRequestSortProperty? sortByProperty,
    SortDirection? sortDirection = SortDirection.Descending)
    {
        var expression = sortByProperty.HasValue ? GetExpression(sortByProperty.Value) : x => x.CreatedAtTimestamp;

        return sortDirection == SortDirection.Descending
            ? queryable.OrderByDescending(expression)
            : queryable.OrderBy(expression);
    }

    private static Expression<Func<QuotationRequest, object>> GetExpression(QuotationRequestSortProperty sortByProperty)
    {
        switch (sortByProperty)
        {
            case QuotationRequestSortProperty.CargoType:
                return x => x.Cargo.Type;
            case QuotationRequestSortProperty.StartingLocationType:
                return x => x.StartingLocation.Type;
            case QuotationRequestSortProperty.FinalLocationType:
                return x => x.FinalLocation.Type;
            case QuotationRequestSortProperty.StartPortName:
                return x => x.StartingLocation.Port == null ? x : x.StartingLocation.Port.Name;
            case QuotationRequestSortProperty.FinalPortName:
                return x => x.FinalLocation.Port == null ? x : x.FinalLocation.Port.Name;
            case QuotationRequestSortProperty.IsPriorityShipment:
                return x => x.IsPriorityShipment;
            case QuotationRequestSortProperty.StartGeoLocationAddress:
                return x => x.StartingLocation.GeoLocation == null ? x : x.StartingLocation.GeoLocation.Address;
            case QuotationRequestSortProperty.FinalGeoLocationAddress:
                return x => x.FinalLocation.GeoLocation == null ? x : x.FinalLocation.GeoLocation.Address;
            case QuotationRequestSortProperty.Status:
                return x => x.Status;
            case QuotationRequestSortProperty.TotalWeight:
                return x => x;
            case QuotationRequestSortProperty.TransportationMode:
                return x => x.TransportationMode;
            default: return x => x.CreatedAtTimestamp;
        }
    }
}