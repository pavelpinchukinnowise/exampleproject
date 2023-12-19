namespace Forwarding.Management.Domain.Location.Model;

public class GeoLocation
{
    public required string Address { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }

    public required string CountryCode { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
