namespace Forwarding.Management.Application.Quotation.Contracts;
public interface IReadStorageService<T>
{
    public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken ct = default);
}
