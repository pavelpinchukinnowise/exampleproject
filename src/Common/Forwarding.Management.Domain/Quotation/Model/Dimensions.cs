using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public record Dimensions
{
    public double? Length { get; init; }

    public double? Width { get; init; }

    public double? Height { get; init; }

    public LengthUnit? Unit { get; init; }
}
