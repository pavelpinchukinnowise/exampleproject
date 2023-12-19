using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Location.Model;
public class Port
{
    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? PostalCode { get; set; }
    public string? Address { get; set; }
    public required string Country { get; set; }
    public required string CountryCode { get; set; }
    public required string InternationalCode { get; set; }
    public required string Name { get; set; }
    public PortType Type { get; set; }
}
