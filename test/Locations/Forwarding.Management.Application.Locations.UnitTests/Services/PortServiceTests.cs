using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;
using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Application.Locations.UnitTests.Services;
public class PortServiceTests
{
    [Fact]
    public async Task GetPortsByFilterAsync_CorrectRequest_NotEmptyArray()
    {
        var query = "ant";
        var service = new PortsService();
        var res = await service.GetPortsByFiltersAsync(query, TransportationMode.Intermodal);
        Assert.Equal(1, res.Count);
    }
}
