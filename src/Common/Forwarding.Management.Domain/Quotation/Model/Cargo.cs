using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class Cargo
{
    public CargoType Type { get; set; }

    public IReadOnlyCollection<Container>? Containers { get; set; }

    public IReadOnlyCollection<BulkItem>? BulkItems { get; set; }

    public IReadOnlyCollection<LooseGoodsItem>? LooseGoodsItems { get; set; }

    public CargoSpecifications? Specifications { get; set; }
}
