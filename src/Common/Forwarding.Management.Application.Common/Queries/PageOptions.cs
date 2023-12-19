namespace Forwarding.Management.Application.Common.Queries;

public record PageOptions
{
    public string? ContinuationToken { get; init; }

    public int? MaxPerPage { get; init; }
}
