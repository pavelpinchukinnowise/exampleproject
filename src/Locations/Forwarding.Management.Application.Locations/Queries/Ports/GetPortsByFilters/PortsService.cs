using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Location.Contracts;
using Forwarding.Management.Domain.Location.Model;

namespace Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;

public class PortsService : IPortsService
{
    private readonly Port[] Ports =
    {
        new()
        {
            Id = 1,
            Address = "Oregonstraat, 2030 Antwerpen", Country = "Belgium", Latitude = 51.246664f,
            Type = PortType.Sea,
            Longitude = 4.402453f, PostalCode = "", CountryCode = "BEL",
            InternationalCode = "BEANR", Name = "Antwerp"
        },
        new()
        {
            Id = 2,
            Address = "Esplanadestraat 36, 9300 Aalst", Country = "Belgium", Latitude = 50.941259f,
            Longitude = 4.038796f,
            PostalCode = "", CountryCode = "BEL", InternationalCode = "BEAAB", Name = "Aalst",
            Type = PortType.Sea
        },
        new()
        {
            Id = 3,
            Address = "Steenstraat 6B, 8211 Zedelgem", Country = "Belgium", Latitude = 51.119303f,
            Longitude = 3.08883f,
            PostalCode = "", CountryCode = "BEL", InternationalCode = "BEART", Name = "Aartrijke",
            Type = PortType.Sea
        },
        new()
        {
            Id = 4,
            Address = "Wybrzeża 1, 11-200 Bartoszyce", Country = "Poland", Latitude = 54.252916f,
            Longitude = 20.811096f, PostalCode = "", CountryCode = "POL",
            InternationalCode = "PLBAR", Name = "Bartoszyce",
            Type = PortType.Sea
        },
        new()
        {
            Id = 5,
            Address = "Jana Sobieskiego 24d, 15-014 Białystok", Country = "Poland",
            Latitude = 53.138198f,
            Longitude = 23.173944f, PostalCode = "", CountryCode = "POL",
            InternationalCode = "PLBIA", Name = "Bialystok",
            Type = PortType.Sea
        },
        new()
        {
            Id = 6, Latitude = 37.7749, Longitude = -122.4194,
            PostalCode = "94128",
            Address = "San Francisco International Airport, San Francisco, CA",
            Country = "United States",
            CountryCode = "US", InternationalCode = "SFO",
            Name = "San Francisco International Airport", Type = PortType.Air
        },
        new()
        {
            Id = 7, InternationalCode = "NYC", Latitude = 52.3667, Longitude = 4.8945,
            PostalCode = "1012 JS",
            Address = "Dam Square, Amsterdam, Netherlands", Country = "Netherlands",
            CountryCode = "NL",
            Name = "Amsterdam Central Station", Type = PortType.Inland
        }
    };

    public async Task<IReadOnlyCollection<Port>> GetPortsByFiltersAsync(string query, TransportationMode? transportationMode = default,
         CancellationToken cancellationToken = default)
    {
        var queryToUpper = query.ToUpperInvariant();

        var portType = GetPortTypeByTransportationMode(transportationMode);

        var results = Ports
           .Where(x => !portType.Any() || portType.Contains(x.Type))
           .Where(x => x.Name.ToUpperInvariant()
           .Contains(queryToUpper) || x.InternationalCode.ToUpperInvariant()
           .Contains(queryToUpper)).ToList();

        return await Task.FromResult(results);
    }

    public Task<Port?> GetPortByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Ports.SingleOrDefault(x => x.Id == id));
    }

    public static PortType[] GetPortTypeByTransportationMode(TransportationMode? transportationMode) => transportationMode switch
    {
        TransportationMode.ByAir => new[] { PortType.Air },
        TransportationMode.ByOcean => new[] { PortType.Sea },
        TransportationMode.ByBarge => new[] { PortType.Inland, PortType.Sea },
        _ => Array.Empty<PortType>()
    };
}
