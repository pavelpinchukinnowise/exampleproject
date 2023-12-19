using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Request;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Response;
using MediatR;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes;
public class ListQuotesQueryHandler : IRequestHandler<ListQuotesQuery, ListQuotesQueryResponse>
{
    private readonly IListQuotesStorageService storageService;
    public ListQuotesQueryHandler(IListQuotesStorageService storageService)
    {
        this.storageService = storageService;
    }
    public async Task<ListQuotesQueryResponse> Handle(ListQuotesQuery request, CancellationToken cancellationToken)
    {

        var pageOptions = new PageOptions
        {
            ContinuationToken = request.PageContinuationToken,
            MaxPerPage = request.PageMaxItems
        };

        var page = await this.storageService.GetQuotationsAsync(pageOptions, cancellationToken);

        var quotationItems = page.Items.Select(x => new QuotesItem()
        {
            Id = x.Id,
            Cargo = x.Cargo,
            CreatedAtTimestamp = x.CreatedAtTimestamp,
            Currency = x.Currency,
            ExchangeRate = x.ExchangeRate,
            Number = x.Number,
            FinalLocation = x.FinalLocation,
            IsPriorityShipment = x.IsPriorityShipment,
            StartingLocation = x.StartingLocation,
            Status = x.Status,
            StatusModifications = x.StatusModifications,
            TotalCostExcludingVAT = x.TotalCostExcludingVAT,
            TotalCost = x.TotalCost,
            TotalPriceExcludingVAT = x.TotalPriceExcludingVAT,
            TotalPrice = x.TotalPrice,
            TransportationMode = x.TransportationMode,
            ValidUntil = x.ValidUntil
        }).ToList();

        var response = new ListQuotesQueryResponse()
        {
            Items = quotationItems,
            ContinuationToken = page.ContinuationToken,
        };
        return response;
    }
}
