using Forwarding.Management.Domain.Enums;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;

namespace Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Requests;
public class QuotationRequestsSortingOptions
{
    public QuotationRequestSortProperty? SortByProperty { get; set; }
    public SortDirection? SortDirection { get; set; }
}
