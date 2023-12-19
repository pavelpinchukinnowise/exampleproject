using Forwarding.Management.Domain.Enums;
using Forwarding.Management.Domain.Location.Model;

namespace Forwarding.Management.Domain.Location.Contracts;

public interface IPortsService
{
    Task<IReadOnlyCollection<Port>> GetPortsByFiltersAsync(string query, TransportationMode? transportationMode = default,
          CancellationToken cancellationToken = default);

    Task<Port?> GetPortByIdAsync(long id, CancellationToken cancellationToken = default);
}