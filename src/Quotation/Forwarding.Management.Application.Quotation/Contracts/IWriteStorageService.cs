namespace Forwarding.Management.Application.Quotation.Contracts;

public interface IWriteStorageService<T>
{
    Task<T> UpsertAsync(T item, CancellationToken cancellationToken);
}
