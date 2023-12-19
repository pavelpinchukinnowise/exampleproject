namespace Forwarding.Management.Domain.Abstractions;

public record StatusModification<T>(T Status, DateTimeOffset Timestamp) where T : Enum;
