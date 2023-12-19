using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;
using Forwarding.Management.Application.Quotation.Constants;
using Forwarding.Management.Domain.Abstractions;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;

public class WithdrawQuotationRequestByIdStorageService : IWithdrawQuotationRequestByIdStorageService
{
    private readonly Container quotationRequestsContainer;

    public WithdrawQuotationRequestByIdStorageService(CosmosClient cosmos)
    {
        quotationRequestsContainer = cosmos
            .GetDatabase(id: DatabaseConstants.DatabaseName)
            .GetContainer(id: DatabaseConstants.QuotationRequestsContainerName);
    }

    public async Task UpdateAsync(string id, CancellationToken cancellationToken)
    {
        QuotationRequest quotationRequest = await quotationRequestsContainer
            .ReadItemAsync<QuotationRequest>(id, PartitionKey.Null, cancellationToken: cancellationToken);

        quotationRequest.Status = QuotationRequestStatus.Withdrawn;

        quotationRequest.StatusModifications.Add(
            new StatusModification<QuotationRequestStatus>(QuotationRequestStatus.Withdrawn, DateTimeOffset.UtcNow));

        await quotationRequestsContainer.UpsertItemAsync(quotationRequest, cancellationToken: cancellationToken);
    }

    public async Task<bool> IsItemWithIdExistingAsync(string id, CancellationToken cancellationToken)
    {
        var response = await quotationRequestsContainer
            .ReadItemStreamAsync(id, PartitionKey.Null, cancellationToken: cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> IsInPendingStatusAsync(string id, CancellationToken cancellationToken)
    {
        QuotationRequest response = await quotationRequestsContainer
            .ReadItemAsync<QuotationRequest>(id, PartitionKey.Null, cancellationToken: cancellationToken);

        return response.Status == QuotationRequestStatus.Pending;
    }
}
