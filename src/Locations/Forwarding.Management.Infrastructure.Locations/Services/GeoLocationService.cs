using System.Globalization;
using Azure;
using Azure.Core.GeoJson;
using Azure.Maps.Routing;
using Azure.Maps.Search;
using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Domain.Location.Model;

namespace Forwarding.Management.Infrastructure.Locations.Services;

public class GeoLocationService : IGeoLocationService
{
    private readonly MapsSearchClient searchClient;
    private readonly MapsRoutingClient routingClient;

    public GeoLocationService(MapsSearchClient searchClient, MapsRoutingClient routingClient)
    {
        this.searchClient = searchClient;
        this.routingClient = routingClient;
    }

    public async Task<IReadOnlyCollection<GeoLocation>> GetLocationsByTermAsync(string query,
        CancellationToken cancellationToken = default)
    {
        var searchResult =
            await searchClient.SearchAddressAsync(query, cancellationToken: cancellationToken);

        return searchResult?.Value?.Results?.Select(x => new GeoLocation
        {
            Address = x.Address.FreeformAddress,
            Latitude = x.Position.Latitude,
            Longitude = x.Position.Longitude,
            PostalCode = x.Address.PostalCode,
            CountryCode = x.Address.CountryCode,
            Country = x.Address.Country
        }).ToList() ?? new List<GeoLocation>();
    }

    public async Task<IReadOnlyCollection<GeoLocation>> GetLocationsByCoordinateAsync(double lat,
        double lon,
        CancellationToken cancellationToken = default)
    {
        var searchResult =
            await searchClient.ReverseSearchAddressAsync(
                new ReverseSearchOptions { Coordinates = new GeoPosition(lon, lat) },
                cancellationToken);

        return searchResult?.Value?.Addresses?.Select(x => new GeoLocation
        {
            Address = x.Address.FreeformAddress,
            Latitude = Convert.ToDouble(x.Position.Split(',')[0], CultureInfo.CurrentCulture),
            Longitude = Convert.ToDouble(x.Position.Split(',')[1], CultureInfo.CurrentCulture),
            PostalCode = x.Address.PostalCode,
            CountryCode = x.Address.CountryCode,
            Country = x.Address.Country
        }).ToList() ?? new List<GeoLocation>();
    }

    public async Task<bool> IsPossibleLandConnectionAsync(Coordinate startLocation, Coordinate endLocation, CancellationToken cancellationToken = default)
    {
        try
        {
            var position = new List<GeoPosition>
            {
                new GeoPosition(startLocation.Longitude, startLocation.Latitude),
                new GeoPosition(endLocation.Longitude, endLocation.Latitude)
            };

            var directions = await routingClient.GetDirectionsAsync(new RouteDirectionQuery(position), cancellationToken);
            return directions.Value.Routes.Any();
        }
        catch (RequestFailedException)
        {
            return false;
        }
    }
}