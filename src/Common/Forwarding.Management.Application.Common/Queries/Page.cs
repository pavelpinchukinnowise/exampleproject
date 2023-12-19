namespace Forwarding.Management.Application.Common.Queries;

public record Page<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }

    public string? ContinuationToken { get; init; }
}
