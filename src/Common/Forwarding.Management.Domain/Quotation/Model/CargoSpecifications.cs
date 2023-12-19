using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class CargoSpecifications
{
    public Dimensions? Dimensions { get; set; }

    public double? Volume { get; set; }

    public VolumeUnit? VolumeUnit { get; set; }

    public required double Weight { get; set; }

    public required WeightUnit WeightUnit { get; set; }
}
