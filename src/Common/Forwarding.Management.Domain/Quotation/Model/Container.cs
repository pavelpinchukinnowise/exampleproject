using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class Container
{
    public int Amount { get; set; }

    public ContainerType Type { get; set; }

    public ContainerLength Length { get; set; }

    public ContainerLoadingMode LoadingMode { get; set; }

    public required CargoSpecifications Specifications { get; set; }
}
