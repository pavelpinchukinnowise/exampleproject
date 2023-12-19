namespace Forwarding.Management.Domain.Abstractions;

public class Entity
{
    public required string Id { get; set; }

    public string? PartitionKey { get; set; }
}
