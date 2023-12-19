using Forwarding.Management.Application.Quotation.Constants;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;

namespace Forwarding.Management.Application.Quotation;

public class LocalDbInitializationService : IHostedService
{
    private readonly CosmosClient cosmosClient;

    public LocalDbInitializationService(CosmosClient cosmosClient)
    {
        this.cosmosClient = cosmosClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseConstants.DatabaseName, cancellationToken: cancellationToken);
        var database = cosmosClient.GetDatabase(DatabaseConstants.DatabaseName);

        await database.CreateContainerIfNotExistsAsync(
            id: DatabaseConstants.QuotationRequestsContainerName,
            partitionKeyPath: DatabaseConstants.PartitionKeyPath,
            cancellationToken: cancellationToken);

        await database.CreateContainerIfNotExistsAsync(
           id: DatabaseConstants.QuotesContainerName,
           partitionKeyPath: DatabaseConstants.PartitionKeyPath,
           cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
