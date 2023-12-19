using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
using Forwarding.Management.Application.Quotation.Constants;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;

namespace Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
public class DeleteQuotationRequestByIdStorageService : IDeleteQuotationRequestByIdService
{
    private readonly Microsoft.Azure.Cosmos.Container quotationRequestsContainer;

    public DeleteQuotationRequestByIdStorageService(CosmosClient cosmos)
    {
        quotationRequestsContainer = cosmos
            .GetDatabase(id: DatabaseConstants.DatabaseName)
            .GetContainer(id: DatabaseConstants.QuotationRequestsContainerName);
    }

    public async Task<QuotationRequest> DeleteByIdAsync(string id, CancellationToken ct)
    {
        var quotationRequest = await quotationRequestsContainer
            .ReadItemAsync<QuotationRequest>(
                id,
                PartitionKey.Null,
                cancellationToken: ct);

        await quotationRequestsContainer
            .DeleteItemAsync<QuotationRequest>(
                id,
                PartitionKey.Null,
                cancellationToken: ct);

        return quotationRequest;
    }

    public async Task<bool> ItemExistsWithIdAsync(string id, CancellationToken ct = default)
    {
        var response = await quotationRequestsContainer
            .ReadItemStreamAsync(
                id,
                PartitionKey.Null,
                cancellationToken: ct);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> IsInWithdrawnStatusAsync(string id,
        CancellationToken ct = default)
    {
        // Legitimate use of type instead of "var", because we're interested in the
        // model and not the default return type which is ItemResponse<QuotationRequest>
        QuotationRequest response = await quotationRequestsContainer
            .ReadItemAsync<QuotationRequest>(
                id,
                PartitionKey.Null,
                cancellationToken: ct);

        return response.Status == QuotationRequestStatus.Withdrawn;
    }
}
