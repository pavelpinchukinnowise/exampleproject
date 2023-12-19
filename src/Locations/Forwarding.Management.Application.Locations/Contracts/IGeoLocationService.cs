using Forwarding.Management.Domain.Location.Model;

namespace Forwarding.Management.Application.Locations.Contracts;

public interface IGeoLocationService
{
    Task<IReadOnlyCollection<GeoLocation>> GetLocationsByTermAsync(string query,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<GeoLocation>> GetLocationsByCoordinateAsync(double lat, double lon,
        CancellationToken cancellationToken = default);

    Task<bool> IsPossibleLandConnectionAsync(Coordinate startLocation, Coordinate endLocation,
    CancellationToken cancellationToken = default);
}