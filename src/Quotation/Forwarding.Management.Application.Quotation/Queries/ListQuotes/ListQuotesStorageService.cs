using System.Text;
using Forwarding.Management.Application.Common.Queries;
using Forwarding.Management.Application.Quotation.Constants;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Contracts;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotes;
public class ListQuotesStorageService : IListQuotesStorageService
{
    private readonly Microsoft.Azure.Cosmos.Container quotesContainer;

    public ListQuotesStorageService(CosmosClient cosmos)
    {
        quotesContainer = cosmos
            .GetDatabase(id: DatabaseConstants.DatabaseName)
            .GetContainer(id: DatabaseConstants.QuotesContainerName);
    }
    public async Task<Page<Quote>> GetQuotationsAsync(PageOptions pageOptions, CancellationToken cancellationToken)
    {
        var requestOptions = new QueryRequestOptions { MaxItemCount = pageOptions?.MaxPerPage ?? -1 };

        var queryable = quotesContainer
            .GetItemLinqQueryable<Quote>(
                continuationToken: DecodeContinuationToken(pageOptions?.ContinuationToken),
                requestOptions: requestOptions)
            .AsQueryable();

        var result = await queryable.ToFeedIterator().ReadNextAsync(cancellationToken);

        return new Page<Quote>
        {
            Items = result.Resource.ToList(),
            ContinuationToken = EncodeContinuationToken(result.ContinuationToken)
        };

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
}
