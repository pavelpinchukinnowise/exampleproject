using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Location.Model;

namespace Forwarding.Management.Domain.Quotation.Model;

public class Location
{
    public LocationType Type { get; set; }
    public long? PortId { get; set; }
    public Port? Port { get; set; }
    public GeoLocation? GeoLocation { get; set; }
}
