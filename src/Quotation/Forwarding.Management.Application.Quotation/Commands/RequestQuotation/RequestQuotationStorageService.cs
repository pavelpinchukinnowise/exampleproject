using Forwarding.Management.Application.Quotation.Constants;
using Forwarding.Management.Application.Quotation.Contracts;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Forwarding.Management.Application.Quotation.Commands.RequestQuotation;

internal sealed class RequestQuotationStorageService : IWriteStorageService<QuotationRequest>
{
    private readonly Container quotationRequestsContainer;

    public RequestQuotationStorageService(CosmosClient cosmos)
    {
        quotationRequestsContainer = cosmos
            .GetDatabase(id: DatabaseConstants.DatabaseName)
            .GetContainer(id: DatabaseConstants.QuotationRequestsContainerName);
    }

    public Task<QuotationRequest> UpsertAsync(QuotationRequest item, CancellationToken cancellationToken)
    {
        return quotationRequestsContainer
            .UpsertItemAsync(item, cancellationToken: cancellationToken)
            .ContinueWith(task => task.Result.Resource, cancellationToken);
    }
}
