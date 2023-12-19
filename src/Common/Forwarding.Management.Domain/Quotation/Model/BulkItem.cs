using Forwarding.Management.Domain.Enums;

namespace Forwarding.Management.Domain.Quotation.Model;

public class BulkItem
{
    public int Amount { get; set; }

    public required string Name { get; set; }

    public Unit Unit { get; set; }
}
