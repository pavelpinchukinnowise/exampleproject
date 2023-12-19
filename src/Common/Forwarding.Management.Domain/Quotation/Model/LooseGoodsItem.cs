using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class LooseGoodsItem
{
    public int Amount { get; set; }

    public required string Name { get; set; }

    public PackageType PackageType { get; set; }

    public CargoSpecifications? Specifications { get; set; }
}
