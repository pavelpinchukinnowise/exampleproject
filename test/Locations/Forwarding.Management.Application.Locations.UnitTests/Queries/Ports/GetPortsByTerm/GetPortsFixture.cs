using Forwarding.Management.Application.Locations.Queries.Ports.DTOs;
using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;
using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Location.Contracts;
using Forwarding.Management.Domain.Location.Model;
using Moq;

namespace Forwarding.Management.Application.Locations.UnitTests.Queries.Ports.GetPortsByTerm;

public class GetPortsFixture
{
    protected static IPortsService CreatePortsService(GetPortsByFiltersQuery query)
    {
        var moqPortsService =
            new Mock<IPortsService>();
        moqPortsService
            .Setup(x => x.GetPortsByFiltersAsync(query.Query!, It.IsAny<TransportationMode?>(), CancellationToken.None))
            .ReturnsAsync(GetLocationsDTO());
        return moqPortsService.Object;
    }

    public virtual async Task<IReadOnlyCollection<PortDto>> ExecuteRequestAsync(GetPortsByFiltersQuery query,
        CancellationToken cancellationToken = default)
    {
        var portService = CreatePortsService(query);
        var handler = new GetPortsByFiltersHandler(portService);

        return await handler.Handle(query, cancellationToken);
    }

    private static IReadOnlyCollection<Port> GetLocationsDTO() =>
        new Port[]
        {
            new()
            {
                Address = "frankw", Country = "Poland", Latitude = 123, Longitude = 57,
                PostalCode = "456", CountryCode = "PO", InternationalCode = "123", Name = "123", Type = PortType.Air,Id=1
            },
            new()
            {
                Address = "paris", Country = "France", Latitude = 34, Longitude = 78,
                PostalCode = "4567", CountryCode = "PO", InternationalCode = "124", Name = "124", Type = PortType.Air,Id=2
            }
        };
}