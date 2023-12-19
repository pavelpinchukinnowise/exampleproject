namespace Forwarding.Management.Application.Locations.Queries.Locations.GetLocationsConnectionEndpoint;

public record GetLocationsConnectionResponse
{
    public required bool Land { get; init; }

    public bool Sea { get; init; }

    public bool Air { get; init; }
}
